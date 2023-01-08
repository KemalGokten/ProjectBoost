using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _mainThrust = 100f;

    [SerializeField]
    private float _rotationThrust = 1f;

    [SerializeField]
    private AudioClip _mainEngine;

    private Rigidbody _rigidbody;

    private AudioSource _audioSource;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        processThrust();
        processRotation();
    }
     
    private void processThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(_mainEngine);
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void processRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            applyRotation(_rotationThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            applyRotation(-_rotationThrust);
        }
    }

    private void applyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; // freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false; // unfreezing rotation so physics system can take over.
    }
}
