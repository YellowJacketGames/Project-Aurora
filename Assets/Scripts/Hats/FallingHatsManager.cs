using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingHatsManager : MonoBehaviour
{
    [SerializeField] private FallingHat.Axis axis;
    [SerializeField] private GameObject hatPrefab;
    [SerializeField] private int initialPoolSize = 35;
    [Header("if player is walking, spawn speed")]
    [SerializeField] private float w_spawnSpeed = 5f;
    [Header("if player is running, spawn speed")]   
    [SerializeField] private float r_spawnSpeed = 10f;
    private float spawnSpeed = 1f;
    [SerializeField] private List<Material> hatsMaterials;
    [SerializeField] private List<Mesh> hatsMeshes;
    private float _spawnTime = 3f;
    private float _elapsedTime;
    [SerializeField] private float _deviationMagnitude = 10.0f;
    [SerializeField] private Vector3 deviation;
    public bool stopHats;

    private LevelObjectPoolingManager pooling;


    private void Start()
    {
        UpdatePosition();
        pooling = GameManager.instance.currentLevelObjectPoolingManager;
        Pool pool = new Pool("Hats", hatPrefab, initialPoolSize);
        pooling.CreateNewPool(pool);
        pooling.FallingHatsManagerRef = this;
    }

    private void OnEnable()
    {
        GameManager.instance.currentController.playerMovementComponent.OnTargetSpeedChanged += HandleSpeedChange;
    }
    private void OnDisable()
    {
        GameManager.instance.currentController.playerMovementComponent.OnTargetSpeedChanged -= HandleSpeedChange;

    }
    private void HandleSpeedChange(float newSpeed)
    {
        spawnSpeed = newSpeed > 10 ? r_spawnSpeed : w_spawnSpeed; //running
    }

    private void Update()
    {
        if (stopHats)
        {
            StopActiveHats();
            return;
        }

        _elapsedTime += Time.deltaTime;
        if (!((_elapsedTime * spawnSpeed) >= _spawnTime)) return;
        _spawnTime = Random.Range(3.0f, 5.0f);
        var randomRotY = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        var hat = pooling.SpawnFromPool("Hats", GetVectorCorrection(), randomRotY);
        hat.GetComponent<FallingHat>().InjectRef(this, axis, hatsMaterials[Random.Range(0, hatsMaterials.Count)],
            hatsMeshes[Random.Range(0, hatsMeshes.Count)]);
        _elapsedTime = 0f;
    }

    private bool disableObjectsOnce = false;

    private void StopActiveHats()
    {
        if (disableObjectsOnce) return;
        foreach (var hat in pooling.GetPool("Hats"))
        {
            hat.GetComponent<FallingHat>().finish = true;
            disableObjectsOnce = true;
        }
    }

    public void SetAxis(FallingHat.Axis axis)
    {
        this.axis = axis;
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (axis == FallingHat.Axis.X)
        {
            transform.localPosition = new Vector3(3, 15, 0);
        }
        else
        {
            transform.localPosition = new Vector3(3, 10, 0);
        }
    }

    public void ReturnToPool(FallingHat hat)
    {
        pooling.ReturnToPool(hat.gameObject);
    }

    private Vector3 GetVectorCorrection()
    {
        deviation = Vector3.zero;
        var parentRot = transform.parent.rotation.eulerAngles.y;
        if (axis.Equals(FallingHat.Axis.X))
        {
            if (parentRot.Equals(90)) deviation.x = Random.Range(3, _deviationMagnitude);
            else deviation.x = Random.Range(-_deviationMagnitude, -3);
        }
        else
        {
            if (parentRot.Equals(0)) deviation.z = Random.Range(3, _deviationMagnitude);
            else deviation.z = Random.Range(-_deviationMagnitude, -3);
        }

        return transform.position + deviation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}