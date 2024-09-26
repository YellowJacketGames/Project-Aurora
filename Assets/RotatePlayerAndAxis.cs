using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerAndAxis : MonoBehaviour
{
    [SerializeField] private float angles = -90;
    private float rotationDuration = 1.0f;
    [SerializeField] private bool used;

    private void OnTriggerEnter(Collider other)
    {
        if(used) return;
        if (!other.CompareTag("Player")) return;
        StartCoroutine(RotatePlayerOverTime(other));
        // other.GetComponentInParent<PlayerMovement>().RotateMovementAxis(angles);
        // other.GetComponentInParent<PlayerController>().characterModel.transform.rotation = Quaternion.Euler(0, angles, 0) * other.GetComponentInParent<PlayerController>().characterModel.transform.rotation;
      GameManager.instance.currentController.transform.rotation = Quaternion.Euler(0, angles, 0) * other.transform.rotation;
        used = true;
    }
    
    private IEnumerator RotatePlayerOverTime(Collider player)
    {
        Transform playerTransform = GameManager.instance.currentController.transform;

        Quaternion startRotation = playerTransform.rotation; // Original rotation
        Quaternion targetRotation = Quaternion.Euler(0, angles, 0) * startRotation; // Desired rotation

        float elapsedTime = 0;

        // Smoothly rotate over 'rotationDuration' seconds
        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            playerTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            yield return null; // Wait for the next frame
        }

        // Ensure the final rotation is exactly the target rotation
        playerTransform.rotation = targetRotation;
    }
}
