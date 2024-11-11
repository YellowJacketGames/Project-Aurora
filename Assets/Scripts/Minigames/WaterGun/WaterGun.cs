using System;
using UnityEngine;
using UnityEngine.Events;

public class WaterGun : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem waterParticle; 
    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        waterParticle = GetComponentInChildren<ParticleSystem>();
    }

    public void UpdatePoint(Vector3 newPos)
    {
        Vector3 localPos = lineRenderer.transform.InverseTransformPoint(newPos);
        lineRenderer.SetPosition(1, localPos);
    }

    public void ResetPoint()
    {
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(0));
    }

    public void Shoot()
    {
        //instanciate particle
        waterParticle.Play();
    }

    public void StopShoot()
    {
        //instanciate particle
        waterParticle.Stop();
    }
}