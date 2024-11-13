using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject smallBallPrefab; 
    public LineRenderer lineRenderer;
    public int trajectorySegments = 30; 
    public float launchForceMultiplier = 10f;
    public float maxTrajectoryDistance = 5f; 

    private GameObject smallBall;
    private Rigidbody ballRb;
    private bool isDragging = false;
    private Vector3 startPoint;
    private Vector3 smallBallPoint;

    public bool enabled;
    private BallMinigame _minigame;

    public void InjectParentRef(BallMinigame ballMinigame)
    {
        _minigame = ballMinigame; 
    }
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        lineRenderer.positionCount = 0; 
    }

    void Update()
    {
        if (!enabled) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ball"))
                {
                    startPoint = hit.point;
                    if (smallBall == null)
                        smallBall = Instantiate(smallBallPrefab, startPoint, Quaternion.identity);
                    else
                        smallBall.transform.position = startPoint;
                    isDragging = true;
                }
            }
        }

        if (isDragging && smallBall != null)
        {
            UpdateSmallBallPosition();
            Vector3 launchDirection = (transform.position - smallBall.transform.position).normalized;
            DrawTrajectory(transform.position, launchDirection * launchForceMultiplier);

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                lineRenderer.positionCount = 0; 
                ballRb.velocity = launchDirection * launchForceMultiplier;
                Destroy(smallBall);
                StartToDestroyBall();
            }
        }
    }

    private void StartToDestroyBall()
    {
        enabled = false;
        StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(0.75f);
        _minigame.NewBall();
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    private void UpdateSmallBallPosition()
    {
        if (!enabled) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                Vector3 direction = (hit.point - transform.position).normalized;
                smallBall.transform.position = transform.position + direction * (transform.localScale.x / 2);
                smallBallPoint = smallBall.transform.position;
            }
        }
    }

    private void DrawTrajectory(Vector3 start, Vector3 initialVelocity)
    {
        if (!enabled) return;

        lineRenderer.positionCount = trajectorySegments;

        float timeStep = 0.1f;
        for (int i = 0; i < trajectorySegments; i++)
        {
            float time = i * timeStep;
            Vector3 point = CalculateParabolicPoint(start, initialVelocity, time);
            lineRenderer.SetPosition(i, point);

            if (Vector3.Distance(start, point) > maxTrajectoryDistance)
            {
                lineRenderer.positionCount = i + 1;
                break;
            }
        }
    }

    private Vector3 CalculateParabolicPoint(Vector3 start, Vector3 velocity, float time)
    {
        return start + velocity * time + 0.5f * Physics.gravity * time * time;
    }
}