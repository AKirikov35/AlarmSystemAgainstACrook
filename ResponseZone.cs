using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ResponseZone : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private BoxCollider _collider;

    public event Action<bool> AlarmTriggered;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;

        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Crook>(out _))
            AlarmTriggered?.Invoke(true);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Crook>(out _))
            AlarmTriggered?.Invoke(false);
    }
}