using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResponseZone : MonoBehaviour
{
    private BoxCollider _collider;

    public event Action AlarmActivated;
    public event Action AlarmDeactivated;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Crook>(out _))
            AlarmActivated?.Invoke();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Crook>(out _))
            AlarmDeactivated?.Invoke();
    }
}
