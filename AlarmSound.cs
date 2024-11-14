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

    private void Start()
    {
        SetupAudioSource();
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

    public void SetAlarmState(bool isSpotted)
    {
        StartCoroutineIfNotRunning(ChangeState(isSpotted));
    }

    protected override IEnumerator ChangeState(bool isActive)
    {
        float targetVolume = isActive ? _maxVolume : _minVolume;

        if (!_audioSource.isPlaying)
            _audioSource.Play();

        while (!Mathf.Approximately(_audioSource.volume, targetVolume))
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _soundDelta * Time.deltaTime);

            if (Mathf.Approximately(_audioSource.volume, _minVolume))
            {
                _audioSource.Pause();
                break;
            }

            yield return null;
        }

        _audioSource.volume = targetVolume;

        if (targetVolume > _minVolume)
            _audioSource.UnPause();
    }
}