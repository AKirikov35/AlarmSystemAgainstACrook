using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AlarmColorChanger : AlarmBase
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _alarmColor;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _defaultColor;
    }

    public void ChangeColor(bool isSpotted)
    {
        StartCoroutineIfNotRunning(ChangeState(isSpotted));
    }

    protected override IEnumerator ChangeState(bool isActive)
    {
        Color targetColor = isActive ? _alarmColor : _defaultColor;

        while (!Color.Equals(_renderer.material.color, targetColor))
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, targetColor, Time.deltaTime);
            yield return null;
        }

        _renderer.material.color = targetColor;
    }
}