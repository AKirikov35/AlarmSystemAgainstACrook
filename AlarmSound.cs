using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSound : AlarmBase
{
    [SerializeField] private AudioClip _alarmClip;

    private AudioSource _audioSource;

    private readonly float _maxVolume = 1.0f;
    private readonly float _minVolume = 0.0f;
    private readonly float _soundDelta = 0.2f;

    private void Awake()
    {
        SetupAudioSource();
    }

    public override void AlarmActivated()
    {
        RefreshCoroutine(IncreaseSound());
    }

    public override void AlarmDeactivated()
    {
        RefreshCoroutine(DecreaseSound());
    }

    private void SetupAudioSource()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.volume = _minVolume;
        _audioSource.clip = _alarmClip;
        _audioSource.loop = true;

        if (_alarmClip == null)
            return;
    }

    private IEnumerator IncreaseSound()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _soundDelta * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator DecreaseSound()
    {
        while (_audioSource.volume > _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _soundDelta * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume <= _minVolume)
            _audioSource.Pause();
    }
}
