using System;
using System.Collections;
using Cinemachine;
using UnityEngine;


public class Codex : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    [SerializeField] public CodexElement selectedElement;
    private int currentElementIndex = 0;

    [SerializeField] private CodexElement[] codexElements;
    [SerializeField] private string[] correctPassword;
    [SerializeField] private float max_time = 2f;
    [SerializeField] private float camY;

    private void Awake()
    {
        codexElements = GetComponentsInChildren<CodexElement>();
        selectedElement = codexElements[currentElementIndex];
    }

    private void Start()
    {
        InitCodexElements();
    }

    public void Select(CinemachineVirtualCamera camera)
    {
        foreach (var element in codexElements)
        {
            element.Select();
        }

        TryToMove(camera, false);
    }

    public void SuperSelect(CinemachineVirtualCamera camera)
    {
        foreach (var element in codexElements)
        {
            element.Deselect();
        }
        selectedElement.SuperSelect();

        TryToMove(camera, true);
    }

    public void Deselect()
    {
        foreach (var element in codexElements)
        {
            element.Deselect();
        }
    }

    public void UnlockCodex()
    {
        unlocked = true;
    }

    private void InitCodexElements()
    {
        for (int i = 0; i < codexElements.Length; i++)
        {
            codexElements[i].Init(correctPassword[i]);
        }
    }

    public void MoveLeft()
    {
        currentElementIndex--;
        if (currentElementIndex < 0)
            currentElementIndex = codexElements.Length - 1;
        selectedElement = codexElements[currentElementIndex];
        DeselectCodexesElements();
        selectedElement.SuperSelect();
    }

    public void MoveRight()
    {
        currentElementIndex++;
        if (currentElementIndex > codexElements.Length - 1)
            currentElementIndex = 0;
        selectedElement = codexElements[currentElementIndex];
        DeselectCodexesElements();
        selectedElement.SuperSelect();
    }

    public void MoveUp()
    {
        selectedElement.RotateUp();
    }

    public void MoveDown()
    {
        selectedElement.RotateDown();
    }


    public bool CheckPassword()
    {
        if (!unlocked) return false;
        var correct = true;
        foreach (var codexElement in codexElements)
        {
            if (!correct) continue;
            if (!codexElement.CheckSelfPassword())
                correct = false;
        }

        return correct;
    }

    private void DeselectCodexesElements()
    {
        foreach (var codexElement in codexElements)
        {
            codexElement.Deselect();
        }
    }

    private bool isMovingCamera;

    private void TryToMove(CinemachineVirtualCamera cam, bool superSelect)
    {
        if (isMovingCamera) return;
        var endPos = cam.transform.position;
        endPos = new Vector3(4.15f, camY, endPos.z);
        if (superSelect)
            endPos = new Vector3(3.35f, camY, endPos.z);

        StartCoroutine(SlerpPositionTo(endPos, cam));
    }

    private IEnumerator SlerpPositionTo(Vector3 endPos, CinemachineVirtualCamera cam)
    {
        isMovingCamera = true;
        float elapsed_time = 0f;
        Vector3 startPos = cam.transform.position;

        while (elapsed_time <= max_time)
        {
            cam.transform.position = Vector3.Lerp(startPos, endPos, elapsed_time / max_time);
            elapsed_time += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = endPos;
        isMovingCamera = false;
        yield return null;
    }
}