using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroller : MonoBehaviour
{
    [SerializeField] private RectTransform group;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private float targetYPosition;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private float fadeDuration = 1f;


    private Vector2 initialPosition;
    private Coroutine scrollCoroutine;

    private void Awake()
    {
        if (group != null)
            initialPosition = group.anchoredPosition;
    }

    public void Activate()
    {
        StartCoroutine(FadeIn());
    }

    public void Deactivate()
    {
        group.anchoredPosition = initialPosition;
        StopAllCoroutines();
    }

    private void Scroll()
    {
        if (group == null) return;
        group.anchoredPosition = initialPosition;

        if (scrollCoroutine != null)
            StopCoroutine(scrollCoroutine);

        scrollCoroutine = StartCoroutine(ScrollStuff());
        Debug.LogWarning("calledf");
    }

    private IEnumerator FadeIn()
    {
        Color initialColor = backgroundImage.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            backgroundImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        backgroundImage.color = targetColor;
        Scroll();

        yield return null;
    }

    private IEnumerator ScrollStuff()
    {
        Vector2 targetPosition = new Vector2(initialPosition.x, targetYPosition);
        while (group.anchoredPosition.y < targetPosition.y)
        {
            Debug.LogWarning("ASDASDASD");
            group.anchoredPosition = Vector2.MoveTowards(
                group.anchoredPosition,
                targetPosition,
                speed * Time.deltaTime
            );

            yield return null;
        }

        scrollCoroutine = null;
    }
}