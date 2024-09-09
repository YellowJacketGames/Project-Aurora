using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableElement : MonoBehaviour
{
    [Header("Interactable Variables")] [SerializeField]
    protected string elementName;

    [SerializeField] protected InteractionType elementType;
    [SerializeField] protected InteractablePopup popup;
    public bool hasBeenInteracted;
    public bool ignoreInteraction = false;
    public bool ignorePopup;

    private void Awake()
    {
        popup = GetComponentInChildren<InteractablePopup>();
        if (!popup) return;
        popup.SetElementType(elementType);
        popup.SetElementName(elementName);
    }

    public virtual void OnInteract()
    {
    }

    #region Return Values

    public string GetElementName()
    {
        return elementName;
    }

    public InteractionType GetElementType()
    {
        return elementType;
    }

    public void ShowInteractPrompt()
    {
        if (ignorePopup) return;
        popup.Show();
    }

    public void HideInteractPrompt()
    {
        if (ignorePopup) return;
        popup.Hide();
    }

    #endregion
}