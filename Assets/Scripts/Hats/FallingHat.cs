using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingHat : MonoBehaviour
{
    private FallingHatsManager _manager;

    [SerializeField] private Axis axis;
    [SerializeField] private float fallingSpeed = 1.0f;
    [SerializeField] private float yDisplacement = 1.0f;
    [SerializeField] private float swingAmplitude = 0.5f;
    [SerializeField] private float swingFrequency = 1.0f;
    [SerializeField] private float noiseIntensity = 0.1f;
    [SerializeField] private float noiseSpeed = 1.0f;
    [SerializeField] private float tiltAmount = 15.0f;
    [SerializeField] private float secondaryTiltAmount = 5.0f;
    [SerializeField] private float tiltRandomness = 5.0f;
    private readonly Vector3 _fallDirection = Vector3.down;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    public bool finish;

    [SerializeField] private float despawnTime = 10f;
    private float _time;
    private Vector3 _initialPos;

    private float _randomYRot;

    private float _randomTiltOffset;
    private float _randomSwingFrequency;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshFilter = GetComponentInChildren<MeshFilter>();
    }

    private void OnEnable()
    {
        _initialPos = transform.position;
        _randomTiltOffset = UnityEngine.Random.Range(-tiltRandomness, tiltRandomness);
        _randomSwingFrequency = swingFrequency + UnityEngine.Random.Range(-0.2f, 0.2f);
        _randomYRot = Random.Range(0, 361);
    }
 

    private void Update()
    {
        if (finish)
        {
            _time = 0f;
            transform.position = _initialPos;
            _manager.ReturnToPool(this);
            return;
        }

        if (_time > despawnTime)
        {
            _time = 0f;
            transform.position = _initialPos;
            _manager.ReturnToPool(this);
        }

        _time += Time.deltaTime;

        var verticalMovement = _fallDirection * (yDisplacement * fallingSpeed * Time.deltaTime);

        var swing = Mathf.Sin(_time * swingFrequency) * swingAmplitude;
        var noise = (Mathf.PerlinNoise(_time * noiseSpeed, 0.0f) - 0.5f) * noiseIntensity;
        var totalMovement = new Vector3();
        if (axis == Axis.X)
            totalMovement = verticalMovement + new Vector3(swing + noise, 0, 0);
        else
            totalMovement = verticalMovement + new Vector3(0, 0, swing + noise);
        transform.position += totalMovement;

        ApplyTilt(swing);
    }

    public void InjectRef(FallingHatsManager manager, Axis axis, Material material, Mesh mesh)
    {
        this.axis = axis;
        _manager = manager;
        meshRenderer.material = material;
        meshFilter.mesh = mesh;
    }

    private void ApplyTilt(float swing)
    {
        var tiltAngle = (swing / swingAmplitude) * (tiltAmount + _randomTiltOffset);
        var secondaryTiltAngle = Mathf.Cos(_time * _randomSwingFrequency) * secondaryTiltAmount;

        transform.rotation = axis == Axis.X
            ? Quaternion.Euler(secondaryTiltAngle, _randomYRot, -tiltAngle)
            : Quaternion.Euler(tiltAngle, _randomYRot, secondaryTiltAngle);
    }

    public enum Axis
    {
        X,
        Z
    }
}