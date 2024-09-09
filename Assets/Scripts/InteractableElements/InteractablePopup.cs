using System;
using TMPro;
using UnityEngine;

public class InteractablePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text textDisplayed;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject nameGroup; 
    
    private string name;
    private InteractionType type;

    private void Awake()
    {
        textDisplayed = GetComponentInChildren<TMP_Text>();
        animator = GetComponent<Animator>();
        nameGroup = transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        DisableGo();
        if(!type.Equals(InteractionType.Conversation)) nameGroup.SetActive(false);
        nameGroup.GetComponentInChildren<TMP_Text>().text = name;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

    public void DisableGo()
    {
        gameObject.SetActive(false);
    }

    public void SetElementName(string name) => this.name = name;

    public void SetElementType(InteractionType type) => this.type = type;
}