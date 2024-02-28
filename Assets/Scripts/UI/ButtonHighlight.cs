using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonHighlight : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDeselectHandler
{
    [Header("Button Components")]
    private Image buttonImage;
    [Space]
    [Header("Sprites")]
    [SerializeField] private Sprite onHover;
    [SerializeField] private Sprite onClick;
    [SerializeField] private Sprite onExit;

    private void Start()
    {
        buttonImage = GetComponent<Image>();

    }
    public void OnDeselect(BaseEventData eventData)
    {
        buttonImage.sprite = onExit;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonImage.sprite = onClick;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = onHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = onExit;
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonImage.sprite = onClick;
    }
}
