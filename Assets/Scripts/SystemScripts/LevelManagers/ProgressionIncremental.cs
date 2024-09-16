using UnityEngine;

public class ProgressionIncremental : MonoBehaviour
{
    [SerializeField] private int levelIndexId; //Set each level index in inspector.. p.e lvl 2 -> index 2 

    private void Start()
    {
        if (levelIndexId != GameManager.instance.Data.progressionIndex)
            GameManager.instance.IncrementProgression();
    }
}