using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Can : MonoBehaviour
{
    private BallMinigame _minigame;
    private bool canCheckY;
    private float limitY = 0.65f;
    public bool isTargetRed;

    public void InjectParentRef(BallMinigame ballMinigame)
    {
        _minigame = ballMinigame;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CanLimiter"))
        {
            canCheckY = true;
            StartCoroutine(CheckYCorr());
        }
    }

    private IEnumerator CheckYCorr()
    {
        while (canCheckY)
        {
            if (transform.position.y > limitY) yield return null;
            if (isTargetRed)
                _minigame.CanFallen();
            canCheckY = false;
            gameObject.SetActive(false);
        }

        yield return null;
    }

    private Vector3 initialPos;
    private Quaternion initialRot;

    private void Awake()
    {
        initialRot = transform.rotation;
        initialPos = transform.position;
    }

    public void Reposition()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = initialPos;
        transform.rotation = initialRot;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}