using System.Collections;
using UnityEngine;

public abstract class AlarmBase : MonoBehaviour
{
    protected Coroutine CurrentCoroutine;

    public abstract void Activate();

    public abstract void Deactivate();

    protected void RefreshCoroutine(IEnumerator coroutine)
    {
        if (CurrentCoroutine != null)
            StopCoroutine(CurrentCoroutine);

        CurrentCoroutine = StartCoroutine(coroutine);
    }
}
