using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private ResponseZone _responseZone;
    [SerializeField] private AlarmSound _alarmSound;
    [SerializeField] private AlarmColorChanger _alarmColorChanger;

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
        _alarmSound.Activate();
        _alarmColorChanger.Activate();
    }

    private void AlarmDeactivated()
    {
        _alarmSound.Deactivate();
        _alarmColorChanger.Deactivate();
    }
}
