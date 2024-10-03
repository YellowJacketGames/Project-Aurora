using UnityEngine;
using Random = UnityEngine.Random;

public class FallingHatsManager : MonoBehaviour
{
    [SerializeField] private FallingHat.Axis axis;
    [SerializeField] private GameObject hatPrefab;
    [SerializeField] private int initialPoolSize = 35; 
    [SerializeField] private float spawnSpeed = 1f;
    private float _spawnTime = 3f;
    private float _elapsedTime;
    private float _deviationMagnitude = 8.0f;
    

    private LevelObjectPoolingManager pooling;


    private void Start()
    {
        UpdatePosition();
        pooling = GameManager.instance.currentLevelObjectPoolingManager;
        Pool pool = new Pool("Hats", hatPrefab, initialPoolSize);
        pooling.CreateNewPool(pool);
        pooling.FallingHatsManagerRef = this;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (!((_elapsedTime * spawnSpeed) >= _spawnTime)) return;
        _spawnTime = Random.Range(3.0f, 5.0f);
        var randomRotY = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        var hat = pooling.SpawnFromPool("Hats", GetVectorCorrection(), randomRotY);
        hat.GetComponent<FallingHat>().InjectRef(this, axis);
        _elapsedTime = 0f;
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
            transform.localPosition = GameManager.instance.currentController.characterModel.transform.rotation.y > 0
                ? new Vector3(0, 10, -3)
                : new Vector3(0, 10, 3);
        }
        else
        {
            var rotationY = GameManager.instance.currentController.characterModel.transform.rotation.eulerAngles.y;

            transform.localPosition = Mathf.Approximately(rotationY, 0f)
                ? new Vector3(3, 10, 0)
                : new Vector3(-3, 10, 0);
        }
    }

    public void ReturnToPool(FallingHat hat)
    {
        pooling.ReturnToPool(hat.gameObject);
    }

    private Vector3 GetVectorCorrection()
    {
        var deviation = Vector3.zero;
        if (axis.Equals(FallingHat.Axis.X))
            deviation.x = Random.Range(-_deviationMagnitude, _deviationMagnitude);
        else
            deviation.z = Random.Range(-_deviationMagnitude, _deviationMagnitude);
        return transform.position + deviation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}