using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class IngameTextElement : MonoBehaviour
{
    [SerializeField] private TMP_Text textReference;
    [SerializeField] private string text;
    [Range(0, 42)] [SerializeField] private int id;
    [SerializeField] private Vector4 axis = new Vector4(0, 0, 1, 0);
    [SerializeField] private float speed = 2f; //*100

    [SerializeField] private RectMask2D mask;
    [SerializeField] private bool triggered = false;
    private IngameTextManager ingameTextManager;
    private float elapsed_time;
    [SerializeField] private float amount = 10;

    private void Awake()
    {
        textReference = GetComponentInChildren<TMP_Text>();
        mask = GetComponentInChildren<RectMask2D>();
        ingameTextManager = GetComponentInParent<IngameTextManager>();
        Init();
    }

    private void Init()
    {
        text = ingameTextManager.GetLevelText(id);
        textReference.text = text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
            TriggerAnimShow();
    }

    private void TriggerAnimShow()
    {
        if (triggered) return;
        triggered = true;
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        elapsed_time += Time.deltaTime * speed;
        while (amount >= 0)
        {
            amount -= elapsed_time;
            mask.padding = axis * amount;
            yield return null;
        }

        mask.padding = axis * amount;
        amount = 0;
        elapsed_time = 0;
    }
}