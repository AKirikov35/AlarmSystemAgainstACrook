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

    public override void Activate()
    {
        RefreshCoroutine(ChangeSoundVolume(_maxVolume));
    }

    public override void Deactivate()
    {
        RefreshCoroutine(ChangeSoundVolume(_minVolume));
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

    private IEnumerator ChangeSoundVolume(float targetVolume)
    {
        if (targetVolume > _minVolume && _audioSource.isPlaying == false)
            _audioSource.Play();

        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _soundDelta * Time.deltaTime);
            yield return null;
        }

        if (Mathf.Approximately(_audioSource.volume, _minVolume))
            _audioSource.Pause();
    }
}
