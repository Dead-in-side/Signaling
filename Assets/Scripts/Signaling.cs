using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SignalingTrigger))]

public class Signaling : MonoBehaviour
{
    private AudioSource _audioSource;
    private SignalingTrigger _signalingTrigger;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _volumeSteps = 0.03f;
    private Coroutine _coroutineVolume;

    private void Awake()
    {
        _signalingTrigger = GetComponent<SignalingTrigger>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.volume = _minVolume;
    }

    private void OnEnable()
    {
        _signalingTrigger.ThiefEnter += IncreaseVolume;
        _signalingTrigger.ThiefExit += DecreaseVolume;
    }

    private void OnDisable()
    {
        _signalingTrigger.ThiefEnter -= IncreaseVolume;
        _signalingTrigger.ThiefExit -= DecreaseVolume;
    }

    private void IncreaseVolume()
    {
        if (_coroutineVolume != null)
        {
            StopCoroutine(_coroutineVolume);
        }

        _coroutineVolume = StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void DecreaseVolume()
    {
        if ((_coroutineVolume != null))
        {
            StopCoroutine(_coroutineVolume);
        }

        _coroutineVolume = StartCoroutine(ChangeVolume(_minVolume));
    }

    private IEnumerator ChangeVolume(float volume)
    {
        _audioSource.Play();

        do
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volume, _volumeSteps * Time.deltaTime);

            yield return null;

        } while (_audioSource.volume != _minVolume);

        _audioSource.Stop();
    }
}
