using System;
using TMPro;
using UnityEngine;

public class InteractablePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text textDisplayed;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        textDisplayed = GetComponentInChildren<TMP_Text>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DisableGo();
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
}