using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private const string FriendlyTag = "Friendly";
    private const string FinishPadTag = "FinishPad";

    [SerializeField]
    private float _loadLevelDelay = 1f;

    [SerializeField]
    private AudioClip _success;

    [SerializeField]
    private AudioClip _crash;

    [SerializeField]
    private ParticleSystem _successParticles;

    [SerializeField]
    private ParticleSystem _crashParticles;

    private AudioSource _audioSource;

    private bool _isTransitioning = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTransitioning)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case FriendlyTag:
                Debug.LogFormat("This thing is {0}", FriendlyTag);
                break;
            case FinishPadTag:
                Debug.LogFormat("Congrats, you finished!");
                StartSuccessSequence();
                break;
            default:
                Debug.LogFormat("Sorry you blew up");
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        _isTransitioning = true;

        _audioSource.Stop();
        _audioSource.PlayOneShot(_success);

        _successParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", _loadLevelDelay);
    }

    private void StartCrashSequence()
    {
        _isTransitioning = true;

        _audioSource.Stop();
        _audioSource.PlayOneShot(_crash);

        _crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", _loadLevelDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }
}
