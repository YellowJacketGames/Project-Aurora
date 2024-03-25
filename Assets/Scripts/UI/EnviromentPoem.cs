using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnviromentPoem : MonoBehaviour
{
    [Header("Enviroment poem components")]
    [SerializeField] private Canvas poemCanvas;
    [SerializeField] private TextMeshProUGUI poemTextComponent;
    [TextArea()][SerializeField] private string poemText;
    [Range(0, 2)] [SerializeField] private float poemTextSpeed;

    bool poemTriggered = false;
    void Start()
    {
        poemTriggered = false;
        poemCanvas.worldCamera = Camera.main;
        poemTextComponent.maxVisibleCharacters = 0;
        poemTextComponent.text = poemText;
    }

    public IEnumerator ShowText()
    {
        char[] charTextArray = poemText.ToCharArray();

        if(charTextArray.Length <= 0)
            yield break;

        for (int i = 0; i < charTextArray.Length; i++)
        {
            poemTextComponent.maxVisibleCharacters++;
            yield return new WaitForSeconds(poemTextSpeed);
        }

        poemTextComponent.maxVisibleCharacters = charTextArray.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (poemTriggered)
            return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            poemTriggered = true;
            StartCoroutine(ShowText());
        }
    }

}
