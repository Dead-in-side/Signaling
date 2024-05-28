using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SignalingTrigger))]

public class Signaling : MonoBehaviour
{
    private AudioSource _audioSource;
    private SignalingTrigger _signalingTrigger;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _volumeSteps = 0.003f;
    private Coroutine _coroutinePlay;
    private Coroutine _coroutineStop;

    private void Awake()
    {
        _signalingTrigger = GetComponent<SignalingTrigger>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.volume = _minVolume;
    }

    private void OnEnable()
    {
        _signalingTrigger.ThiefEnter += PlaySound;
        _signalingTrigger.ThiefExit += StopSound;
    }

    private void OnDisable()
    {
        _signalingTrigger.ThiefEnter -= PlaySound;
        _signalingTrigger.ThiefExit -= StopSound;
    }

    private void PlaySound()
    {
        _coroutinePlay = StartCoroutine(ClipPlayCoroutine());

        if (_coroutineStop != null)
        {
            StopCoroutine(_coroutineStop);
        }
    }

    private void StopSound()
    {
        _coroutineStop = StartCoroutine(ClipStopCoroutine());

        if ((_coroutinePlay != null))
        {
            StopCoroutine(_coroutinePlay);
        }
    }

    private IEnumerator ClipPlayCoroutine()
    {
        _audioSource.Play();

        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _volumeSteps);

            yield return null;
        }
    }

    private IEnumerator ClipStopCoroutine()
    {
        while (_audioSource.volume > _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _volumeSteps);

            yield return null;
        }

        _audioSource.Stop();
    }
}
