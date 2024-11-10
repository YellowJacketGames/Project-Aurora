using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Horse : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private Transform target;

    [SerializeField] private int goalTicks = 250;
    [SerializeField] private int currentTicks = 0;
    [SerializeField] private float waveAmplitude = 1f;
    [SerializeField] private float waveFrequency = 2f;
    [SerializeField] private float rotationAmplitude = 15f;
    [SerializeField] private float rotationSpeed = 8f;

    [SerializeField] private int tickIncrement = 1;

    [SerializeField] private bool isRacing = false;
    [SerializeField] private HorseMinigame _minigame;
    private Coroutine tickCorr;

    [ContextMenu("Start Race")]
    public void StartRace()
    {
        isRacing = true;
        tickCorr = StartCoroutine(Tick());
    }

    private void InitSpeedRandomly()
    {
        if (_minigame.selectedHorse == this) return;
        // tickIncrement = Random.Range((int)2, (int)6);
        rotationAmplitude = Random.Range(40, 61);
        rotationSpeed = Random.Range(8, 14);
    }

    public void InjectParentRef(HorseMinigame reference)
    {
        _minigame = reference;
        InitSpeedRandomly();
    }

    public void IncreaseTicks()
    {
        if (!isRacing) return;
        if (currentTicks < goalTicks)
        {
            currentTicks += 20;
        }
    }

    public void Reset()
    {
        currentTicks = 0;
        tickIncrement = Random.Range((int)1, (int)5);
        transform.position = origin.position;
        transform.rotation = quaternion.identity;
        rotationAmplitude = Random.Range(40, 61);
        rotationSpeed = Random.Range(8, 14);
    }

    public void StopRunning()
    {
        StopCoroutine(tickCorr);
    }

    public IEnumerator Tick()
    {
        while (currentTicks < goalTicks)
        {
            float progress = (float)currentTicks / goalTicks;

            Vector3 targetPosition = Vector3.Lerp(origin.position, target.position, progress);

            float waveOffset = Mathf.Sin(progress * Mathf.PI * waveFrequency) * waveAmplitude;
            Vector3 finalPosition = targetPosition + new Vector3(0, waveOffset, 0);
            transform.position = finalPosition;

            float rotationOffset = Mathf.Sin(progress * Mathf.PI * rotationSpeed) * rotationAmplitude;
            transform.localRotation = Quaternion.Euler(rotationOffset, 0, 0);

            if (isRacing)
            {
                currentTicks += tickIncrement;
            }

            yield return new WaitForSeconds(0.02f);
        }

        _minigame.CheckIfWinner(this);
    }

    private void OnDrawGizmos()
    {
        if (origin != null && target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(origin.position, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(target.position, 0.1f);

            // Dibuja la línea de onda entre los puntos
            Gizmos.color = Color.blue;
            Vector3 previousPoint = origin.position;
            int points = 50;
            for (int i = 1; i <= points; i++)
            {
                float t = (float)i / points;
                Vector3 interpolatedPoint = Vector3.Lerp(origin.position, target.position, t);
                float waveOffset = Mathf.Sin(t * Mathf.PI * waveFrequency) * waveAmplitude;
                interpolatedPoint.y += waveOffset;
                Gizmos.DrawLine(previousPoint, interpolatedPoint);
                previousPoint = interpolatedPoint;
            }
        }
    }
}