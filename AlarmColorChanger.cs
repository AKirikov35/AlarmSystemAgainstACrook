using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AlarmColorChanger : AlarmBase
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _alarmColor;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _defaultColor;
    }

    public override void AlarmActivated()
    {
        RefreshCoroutine(ChangeColorToAlarm());
    }

    public override void AlarmDeactivated() 
    {
        RefreshCoroutine(ChangeColorToNormal());
    }

    private IEnumerator ChangeColorToAlarm()
    {
        while (Equals(_renderer.material.color, _alarmColor) == false)
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _alarmColor, Time.deltaTime);

            yield return null;
        }

        _renderer.material.color = _alarmColor;
    }

    private IEnumerator ChangeColorToNormal()
    {
        while (Equals(_renderer.material.color, _defaultColor) == false)
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _defaultColor, Time.deltaTime);

            yield return null;
        }

        _renderer.material.color = _defaultColor;
    }
}
