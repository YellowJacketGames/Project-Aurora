using UnityEngine;

public class ProgressionIncremental : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.IncrementProgression();
    }
}