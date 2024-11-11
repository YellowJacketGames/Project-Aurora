using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaterMinigame : InteractableElement
{
    private int playerLayer;

    [SerializeField] private CinemachineVirtualCamera minigameCamera;
    [SerializeField] private CinemachineVirtualCamera levelCamera;
    [SerializeField] private Camera camera;

    [SerializeField] private WaterGun waterGun;
    [SerializeField] private List<GameObject> hitObjects;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text progressScore;
    [SerializeField] private Image progresBar;
    [SerializeField] private bool winner;
    public LayerMask raycastLayer;
    private bool inMinigame;

    [SerializeField] private float minWaitTime = 0.5f;
    [SerializeField] private float maxWaitTime = 4.0f;

    [SerializeField] private bool isShooting;
    [SerializeField] private float score;
    [SerializeField] private int targetScore = 1000;

    private void OnEnable()
    {
        EventsManager.onMinigamePress.AddListener(ShootWater);
        EventsManager.onMinigamePressCanceled.AddListener(StopShootWater);
        EventsManager.onMinigameExit.AddListener(ExitMinigame);
    }

    private void OnDisable()
    {
        EventsManager.onMinigamePress.RemoveListener(ShootWater);
        EventsManager.onMinigamePressCanceled.RemoveListener(StopShootWater);
        EventsManager.onMinigameExit.RemoveListener(ExitMinigame);
    }


    private void StopShootWater()
    {
        waterGun.StopShoot();
        isShooting = false;
    }

    private void ShootWater()
    {
        if (!inMinigame || winner) return;
        waterGun.Shoot();
        isShooting = true;
    }

    protected override void Awake()
    {
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
        StartCoroutine(StartMinigameCorr());
    }

    private IEnumerator StartMinigameCorr()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        inMinigame = true;
        countdownText.text = "";
        foreach (var hit in hitObjects)
            hit.SetActive(true);
        StartMinigame();
        yield return null;
    }

    private void StartMinigame()
    {
        //read mouse input position 
        StartCoroutine(UpdateMousePos());
        StartCoroutine(UpdateHitObjects());
        //update hosepipe forward to match direction to mouse pos in real world
    }

    private IEnumerator UpdateMousePos()
    {
        while (!winner && inMinigame)
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = camera.ScreenPointToRay(mousePos);
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(ray, 0.5f, Mathf.Infinity, raycastLayer);

            if (hits.Length > 0)
            {
                Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
                RaycastHit closestHit = hits[0];
                Vector3 targetPos = closestHit.point;

                foreach (var hit in hits)
                    if (isShooting && hit.collider.CompareTag("HitPoint"))
                        score += 0.2f;

                waterGun.UpdatePoint(targetPos);
                Vector3 direction = (targetPos - waterGun.transform.position).normalized;
                waterGun.transform.forward = direction;
            }

            UpdateProgressBar();

            yield return null;
        }
    }

    private void UpdateProgressBar()
    {
        var roundedPercentage = Mathf.RoundToInt((score / targetScore) * 100);
        progressScore.text = roundedPercentage + "%";
        progresBar.fillAmount = score / targetScore;
        if (score >= targetScore)
        {
            winner = true;
            countdownText.text = winner ? "Has ganado!" : "Has perdido";
        }
    }

    private IEnumerator UpdateHitObjects()
    {
        while (!winner && inMinigame)
        {
            int randomIndex = Random.Range(0, hitObjects.Count);
            var selected = hitObjects[randomIndex];
            foreach (var hit in hitObjects)
                hit.SetActive(false);

            selected.SetActive(true);
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void ExitMinigame()
    {
        ResetMainCamera();
        ChangeInputScheme(false);
        HideInteractPrompt();
        ignorePopup = false;
        countdownText.text = "";
        winner = false;
        progressScore.text = "0%";
        score = 0;
        progresBar.fillAmount = 0;
        waterGun.ResetPoint();
        inMinigame = false;
        foreach (var hit in hitObjects)
            hit.SetActive(true);
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