using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AlarmColorChanger : AlarmBase
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _alarmColor;

    private Renderer _renderer;

    private readonly float _colorChangeDelta = 0.4f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _defaultColor;
    }

    public override void Activate()
    {
        RefreshCoroutine(ChangeColor(_alarmColor));
    }

    public override void Deactivate() 
    {
        RefreshCoroutine(ChangeColor(_defaultColor));
    }

    private IEnumerator ChangeColor(Color targetColor)
    {
        while (Equals(_renderer.material.color, targetColor) == false)
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, targetColor, _colorChangeDelta * Time.deltaTime);
            yield return null;
        }

        _renderer.material.color = targetColor;
    }
}
