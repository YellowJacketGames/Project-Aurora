using System;
using TMPro;
using UnityEngine;

public class UILanguageAdapter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string espText;
    [SerializeField] private string engText;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        EventsManager.OnLanguageChanged.AddListener(UpdateCurrentText);
    }

    private void OnDestroy()
    {
        EventsManager.OnLanguageChanged.RemoveListener(UpdateCurrentText);
    }

    private void UpdateCurrentText(SavingData.Language arg0)
    {
        if (arg0 == SavingData.Language.ESP)
            _text.text = espText;
        else
            _text.text = engText;
    }
}