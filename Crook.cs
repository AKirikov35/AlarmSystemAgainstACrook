using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Crook : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }
}
