using System.Collections;
using UnityEngine;

public abstract class AlarmBase : MonoBehaviour
{
    protected Coroutine _currentCoroutine;

    protected void StartCoroutineIfNotRunning(IEnumerator coroutine)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(coroutine);
    }

    protected abstract IEnumerator ChangeState(bool isActive);
}