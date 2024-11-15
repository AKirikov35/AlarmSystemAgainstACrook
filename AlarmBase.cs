using System.Collections;
using UnityEngine;

public abstract class AlarmBase : MonoBehaviour
{
    protected Coroutine CurrentCoroutine;

    public abstract void AlarmActivated();

    public abstract void AlarmDeactivated();

    protected void RefreshCoroutine(IEnumerator coroutine)
    {
        if (CurrentCoroutine != null)
            StopCoroutine(CurrentCoroutine);

        CurrentCoroutine = StartCoroutine(coroutine);
    }
}
