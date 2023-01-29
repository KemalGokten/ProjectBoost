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

    [SerializeField]
    private ParticleSystem _mainEngineParticles;

    [SerializeField]
    private ParticleSystem _leftThrusterParticle;

    [SerializeField]
    private ParticleSystem _rightThrusterParticle;

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
            startThrusting();
        }
        else
        {
            stopThrusting();
        }
    }

    private void processRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rotateRight();
        }

        else
        {
            stopRotating();
        }
    }
    private void startThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_mainEngine);
        }

        if (!_mainEngineParticles.isPlaying)
        {
            _mainEngineParticles.Play();
        }
    }
    private void stopThrusting()
    {
        _audioSource.Stop();
        _mainEngineParticles.Stop();
    }

    private void rotateRight()
    {
        applyRotation(-_rotationThrust);
        if (!_leftThrusterParticle.isPlaying)
        {
            _leftThrusterParticle.Play();
        }
    }

    private void rotateLeft()
    {
        applyRotation(_rotationThrust);
        if (!_rightThrusterParticle.isPlaying)
        {
            _rightThrusterParticle.Play();
        }
    }
    private void stopRotating()
    {
        _rightThrusterParticle.Stop();
        _leftThrusterParticle.Stop();
    }

    private void applyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; // freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false; // unfreezing rotation so physics system can take over.
    }
}
