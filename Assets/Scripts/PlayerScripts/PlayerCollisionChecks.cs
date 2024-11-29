using UnityEngine;

public class PlayerCollisionChecks : PlayerComponent
{
    private BoxCollider _boxCollider;
    private Vector3 _boundsSize;
    private Vector3 _center;

    public override void Awake()
    {
        base.Awake();
        _boxCollider = GetComponent<BoxCollider>();
        _boundsSize = _boxCollider.size;
        _center = _boxCollider.center;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Interactable") || other.CompareTag("CameraArea")) return;
        ApplyTriggerBool(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")|| other.CompareTag("Interactable") || other.CompareTag("CameraArea")) return;
        ApplyTriggerBool(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")|| other.CompareTag("Interactable") || other.CompareTag("CameraArea")) return;
        ApplyTriggerBool(false);
    }

    protected virtual void ApplyTriggerBool(bool value)
    {
    }

    public void SetCrouchDimensions()
    {
        _boundsSize = new Vector3(_boundsSize.x, 0.5f, _boundsSize.z);
        _center = new Vector3(_center.x, 0.5f, _center.z);
        _boxCollider.size = _boundsSize;
        _boxCollider.center = _center;
    }

    public void ResetDefaultDimensions()
    {
        _boundsSize = new Vector3(_boundsSize.x, 1.5f, _boundsSize.z);
        _center = new Vector3(_center.x, 1.0f, _center.z);
        _boxCollider.size = _boundsSize;
        _boxCollider.center = _center;
    }
}