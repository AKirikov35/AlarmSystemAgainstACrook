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
        _responseZone.AlarmActivated += AlarmActivated;
        _responseZone.AlarmDeactivated += AlarmDeactivated;
    }

    private void OnDisable()
    {
        _responseZone.AlarmActivated -= AlarmActivated;
        _responseZone.AlarmDeactivated -= AlarmDeactivated;
    }

    private void AlarmActivated()
    {
        _alarmSound.AlarmActivated();
        _alarmColorChanger.AlarmActivated();
    }

    private void AlarmDeactivated()
    {
        _alarmSound.AlarmDeactivated();
        _alarmColorChanger.AlarmDeactivated();
    }
}
