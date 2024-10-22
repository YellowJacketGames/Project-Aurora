using UnityEngine;

public class IngameTextManager : MonoBehaviour
{
    [SerializeField] private string[] level_texts;

    public string GetLevelText(int id)
    {
        return level_texts[id];
    }
}