using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class BallMinigame : InteractableElement
{
    private int playerLayer;
    [SerializeField] private string objectName;
    [Space(10)]
    [SerializeField] private CinemachineVirtualCamera minigameCamera;
    [SerializeField] private CinemachineVirtualCamera levelCamera;
    [SerializeField] private Camera camera;

    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private bool winner;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Ball currentBall;

    [SerializeField] private int usedBalls;
    [SerializeField] private Transform ballInitPos;
    private Can[] _cans;
    [SerializeField] private int remainingCans;


    private Coroutine minigameCorr;
    private bool minigameGaveTicket = false;

    private void OnEnable()
    {
        EventsManager.onMinigameExit.AddListener(ExitMinigame);
    }

    private void OnDisable()
    {
        EventsManager.onMinigameExit.RemoveListener(ExitMinigame);
    }

    public void CheckIfWinner()
    {
        winner = remainingCans == 0;
        countdownText.text = winner ? "Has ganado!" : "Has perdido";
        if (!winner) return;
        if (minigameGaveTicket) return;
        ObjectClass obj = Resources.Load<ObjectClass>($"ScriptableObjects/Objects/KeyObjects/{objectName}");
        if (obj == null)
        {
            Debug.LogError(
                $"El objeto con clave '{objectName}' no se encontró en Resources/ScriptableObjects/Objects/KeyObjects/");
            return;
        }

        if (GameManager.instance.Data.HowManyOf(objectName)>=3) return;
        GameManager.instance.canAddMultipleInstancesOfSameId = true;
        GameManager.instance.currentController.playerInventoryComponent.AddObjectToKeyInventory(obj);
        GameManager.instance.Data.AddObject(objectName);
        GameManager.instance.canAddMultipleInstancesOfSameId = false;
        minigameGaveTicket = true;
        StartCoroutine(GoBack());
        
    }
    private IEnumerator GoBack()
    {
        yield return new WaitForSeconds(1);
        ExitMinigame();
        yield return null;
    }
    protected override void Awake()
    {
        _cans = GetComponentsInChildren<Can>();
        remainingCans = _cans.Count(can => can.isTargetRed);
        foreach (var can in _cans)
            can.InjectParentRef(this);
        base.Awake();
        camera = Camera.main;
        playerLayer = LayerMask.NameToLayer("Player");
    }

    public override void OnInteract()
    {
        SetMinigameCamera();
        ChangeInputScheme(true);
        HideInteractPrompt();
        ignorePopup = true;
        if (minigameCorr != null)
        {
            StopCoroutine(minigameCorr);
            ResetGameStuff();
        }

        minigameCorr = StartCoroutine(StartMinigameCorr());
    }

    private IEnumerator StartMinigameCorr()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "";

        StartMinigame();
        yield return null;
    }

    public void CanFallen()
    {
        if (remainingCans > 1)
            remainingCans--;
        else
        {
            remainingCans--;
            winner = true;
            CheckIfWinner();
        }
    }

    private void StartMinigame()
    {
        var ball = Instantiate(ballPrefab, ballInitPos.position, quaternion.identity);
        currentBall = ball.GetComponent<Ball>();
        currentBall.InjectParentRef(this);
        currentBall.enabled = true;
        usedBalls = 0;
    }

    public void NewBall()
    {
        if (usedBalls < 4) //5 balls used
        {
            var ball = Instantiate(ballPrefab, ballInitPos.position, quaternion.identity);
            currentBall = ball.GetComponent<Ball>();
            currentBall.InjectParentRef(this);
            currentBall.enabled = true;
            usedBalls++;
        }
        else
        {
            StartCoroutine(WaitToEnd());
        }
    }

    private IEnumerator WaitToEnd()
    {
        yield return new WaitForSeconds(1.0f);
        CheckIfWinner();
    }

    private void ResetGameStuff()
    {
        foreach (var can in _cans)
        {
            can.gameObject.SetActive(true);
            can.Reposition();
        }

        countdownText.text = "";
        winner = false;

        if (currentBall)
            Destroy(currentBall.gameObject);
        usedBalls = 0;
        remainingCans = _cans.Count(can => can.isTargetRed);
    }

    public void ExitMinigame()
    {
        ResetMainCamera();
        ChangeInputScheme(false);
        HideInteractPrompt();
        ignorePopup = false;
        ResetGameStuff();
        StopCoroutine(minigameCorr);
    }

    private void SetMinigameCamera()
    {
        camera.cullingMask &= ~(1 << playerLayer);
        minigameCamera.Priority = 5;
        levelCamera.Priority = 0;
    }

    private void ResetMainCamera()
    {
        Camera.main.cullingMask = -1;
        minigameCamera.Priority = 0;
        levelCamera.Priority = 5;
    }

    private void ChangeInputScheme(bool minigameMode)
    {
        if (minigameMode)
            GameManager.instance.currentController.playerInputHandlerComponent.ChangeToMinigamesControls();
        else
            GameManager.instance.currentController.playerInputHandlerComponent.ChangeToLevelControls();
    }
}