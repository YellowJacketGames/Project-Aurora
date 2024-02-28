using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableElement : MonoBehaviour
{
    [Header("Interactable Variables")]
    [SerializeField] protected string elementName;
    [SerializeField] protected InteractionType elementType;
    public bool hasBeenInteracted;
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

    #endregion

}
