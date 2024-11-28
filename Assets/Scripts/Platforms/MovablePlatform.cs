using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    public enum MovementType
    {
        Linear,
        Circular,
        Swing
    }

    [Header("General Settings")] public MovementType movementType;
    public float speed = 2.0f;

    [HideInInspector] public Transform pointA;
    [HideInInspector] public Transform pointB;

    [HideInInspector] public float radius = 5.0f;
    [HideInInspector] public Transform circularCenter;

    [HideInInspector] public Transform anchor;
    public float minAngle = -100f;
    public float maxAngle = 100f;    
    
    
    private Vector3 centerPoint;
    private float circularAngle;
    private bool movingToB = true;

    private List<Transform> passengers;
    private Vector3 previousPosition;

    private PlayerController player;

    private void Start()
    {
        player = GameManager.instance.currentController;
        
        gameObject.tag = "MovablePlatform";
        passengers = new List<Transform>();

        if (movementType == MovementType.Circular && circularCenter != null)
        {
            centerPoint = circularCenter.position;
        }

    }

    private void Update()
    {
        Vector3 deltaPosition = transform.position - previousPosition;
        previousPosition = transform.position;
        if (passengers.Count > 0)
            foreach (Transform passenger in passengers)
            {
                passenger.position += deltaPosition;
            }


        switch (movementType)
        {
            case MovementType.Linear:
                HandleLinearMovement();
                break;
            case MovementType.Circular:
                HandleCircularMovement();
                break;
            case MovementType.Swing:
                HandleSwingMovement();
                break;
        }
    }

    private void HandleSwingMovement()
    {

        if (passengers.Count <= 0) return; 
        var direction = player.transform.position - anchor.position;
        var angle = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        var targetRotation = Quaternion.Euler(angle, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }
    
    
    private void HandleLinearMovement()
    {
        if (pointA == null || pointB == null) return;

        Vector3 target = movingToB ? pointB.position : pointA.position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
            movingToB = !movingToB;
    }

    private void HandleCircularMovement()
    {
        if (circularCenter == null) return;

        circularAngle += speed * Time.deltaTime;
        if (circularAngle >= 360f) circularAngle -= 360f;

        float x = centerPoint.x + Mathf.Cos(circularAngle) * radius;
        float z = centerPoint.z + Mathf.Sin(circularAngle) * radius;
        transform.position = new Vector3(x, transform.position.y, z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            passengers.Add(collision.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            passengers.Remove(collision.transform);
        }
    }

    private void OnDrawGizmos()
    {
        if (movementType == MovementType.Linear && pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
        else if (movementType == MovementType.Circular && circularCenter != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(circularCenter.position, radius);
        }
    }
}