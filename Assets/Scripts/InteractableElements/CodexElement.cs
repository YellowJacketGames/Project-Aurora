using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexElement : MonoBehaviour
{
    [SerializeField] private List<string> displayElements;
    [SerializeField] private string correctElement;

    [SerializeField] private string currentElement;


    [SerializeField] private float _rotationAmount = 45.0f;
    private int _rotationCount = 1;
    private Vector3 rotationAxis = -Vector3.forward;
    [SerializeField] private float max_time = 2f;

    private bool isRotating;

    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material innerSelectedMaterial;
    [SerializeField] private Material defaultMaterial;

    private MeshRenderer _meshRenderer;
    private CodexGroup _parent;

    private void Awake()
    {
        _parent = GetComponentInParent<CodexGroup>();
        _meshRenderer = GetComponent<MeshRenderer>();
        Deselect();
    }

    public void SuperSelect()
    {
        // _meshRenderer.material = innerSelectedMaterial;
        SetOutline(_parent.GetDeciphered());
    }

    public void Select()
    {
        // _meshRenderer.material = selectedMaterial;
        SetOutline(_parent.GetDeciphered());
    }

    public void Deselect()
    {
        // _meshRenderer.material = defaultMaterial;
        UnsetOutline();
    }

    private void SetOutline(bool green)
    {
        if (!green)
        {
            gameObject.layer = 11;
            transform.GetChild(0).gameObject.layer = 11;
        }
        else
        {
            gameObject.layer = 12;
            transform.GetChild(0).gameObject.layer = 12;
        }
    }

    private void SetOutlineAfterCheckPassword(bool passwordOutput)
    {
        if (!passwordOutput) //red outline
        {
            gameObject.layer = 13;
            transform.GetChild(0).gameObject.layer = 13;
        }
        else
        {
            gameObject.layer = 12;
            transform.GetChild(0).gameObject.layer = 12;
        }
    }

    private void UnsetOutline()
    {
        gameObject.layer = 0;
        transform.GetChild(0).gameObject.layer = 0;
    }

    [ContextMenu("RotateUp")]
    public void RotateUp()
    {
        TryToRotate(-1);
    }

    [ContextMenu("RotateDown")]
    public void RotateDown()
    {
        TryToRotate(1);
    }

    private void TryToRotate(int direction)
    {
        if (isRotating) return;
        StartCoroutine(SlerpRotationTo(direction * rotationAxis * _rotationAmount * _rotationCount));
    }

    private IEnumerator SlerpRotationTo(Vector3 toRotation)
    {
        isRotating = true;
        float elapsed_time = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(toRotation);

        while (elapsed_time <= max_time)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed_time / max_time);
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        elapsed_time = max_time;
        transform.rotation = Quaternion.Euler(toRotation);
        _rotationCount++;
        isRotating = false;
        //check pass
        SetOutlineAfterCheckPassword(_parent.CheckPassword());

        yield return null;
    }

    public void Init(string password)
    {
        correctElement = password;
    }

    public bool CheckSelfPassword()
    {
        return currentElement.Equals(correctElement);
    }
}