using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private ResponseZone _responseZone;
    [SerializeField] private AlarmSound _alarmSound;
    [SerializeField] private AlarmColorChanger _alarmColorChanger;

    private void Awake()
    {
        if (_responseZone == null)
            throw new MissingComponentException($"{nameof(ResponseZone)} component is missing from the GameObject: {this.gameObject.name}.");

        if (_alarmSound == null)
            throw new MissingComponentException($"{nameof(AlarmSound)} component is missing from the GameObject: {this.gameObject.name}.");

        if (_alarmColorChanger == null)
            throw new MissingComponentException($"{nameof(AlarmColorChanger)} component is missing from the GameObject: {this.gameObject.name}.");
    }

    private void OnEnable()
    {
        if (_responseZone != null)
            _responseZone.AlarmTriggered += PlayAlarmSound;
    }

    private void OnDisable()
    {
        if (_responseZone != null)
            _responseZone.AlarmTriggered -= PlayAlarmSound;
    }

    private void PlayAlarmSound(bool alarmTriggerActivate)
    {
        if (_alarmSound != null)
            _alarmSound.SetAlarmState(alarmTriggerActivate);

        if (_alarmColorChanger != null)
            _alarmColorChanger.ChangeColor(alarmTriggerActivate);
    }
}