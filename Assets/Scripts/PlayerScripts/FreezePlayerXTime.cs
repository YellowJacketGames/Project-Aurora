using System.Collections;
using UnityEngine;

public class FreezePlayerXTime : MonoBehaviour
{
    [SerializeField] private float freezeTime = 1.0f;
    private Coroutine freezeCorroutine;

    public void TryFreeze()
    {
        if (freezeCorroutine != null)
            StopCoroutine(freezeCorroutine);
        freezeCorroutine = StartCoroutine(FreezePlayerTime(freezeTime));
    }

    private IEnumerator FreezePlayerTime(float time)
    {
        var elapsedTime = 0f;
        GameManager.instance.currentController.playerMovementComponent.DisableAllInput();
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / time);
            yield return null;
        }

        GameManager.instance.currentController.playerMovementComponent.EnableAllInput();
        yield return null;
    }
}