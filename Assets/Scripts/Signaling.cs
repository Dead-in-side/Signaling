using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    private Collider _collider;
    private AudioSource _audioSource;
    private bool _isTriggered = false;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _volumeSteps = 0.003f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void Update()
    {
        if (_isTriggered)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _volumeSteps);
        }
        else
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _volumeSteps);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isTriggered = true;
        _audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        _isTriggered = false;
    }
}
