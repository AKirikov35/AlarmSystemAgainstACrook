using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioClip _alarmClip;

    private Rigidbody _rigidbody;
    private BoxCollider _collider;
    private AudioSource _audioSource;
    private Coroutine _currentCoroutine;

    private readonly float _maxVolume = 1.0f;
    private readonly float _minVolume = 0.0f;
    private readonly float _soundDelta = 0.2f;

    private void Awake()
    {
        InitializeComponents();
        SetupAudioSource();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Crook>(out _))
            SetAlarmState(true);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Crook>(out _))
            SetAlarmState(false);
    }

    private void InitializeComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;

        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void SetupAudioSource()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.volume = _minVolume;
        _audioSource.clip = _alarmClip;
        _audioSource.loop = true;

        if (_alarmClip == null)
            Debug.LogError("Отсутствует аудиоклип", this);
    }

    private void SetAlarmState(bool isSpotted)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(ChangeVolumeCoroutine(isSpotted));
    }

    private IEnumerator ChangeVolumeCoroutine(bool isSpotted)
    {       
        float targetVolume = isSpotted ? _maxVolume : _minVolume;

        if (!_audioSource.isPlaying)
            _audioSource.Play();

        while(!Mathf.Approximately(_audioSource.volume, targetVolume))
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
