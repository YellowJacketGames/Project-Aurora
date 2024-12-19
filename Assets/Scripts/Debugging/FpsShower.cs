using TMPro;
using UnityEngine;

public class FpsShower : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private float pollingTime = .1f;
    private float time;
    private float frameCount;

    private void Update()
    {
        time += Time.deltaTime;
        frameCount++;
        if (time >= pollingTime)
        {
            
            var frameRate = Mathf.RoundToInt(frameCount / time);
            text.text = frameRate + " fps";
            if(frameRate >= 60)
                text.color = Color.green;
            else if(frameRate >= 30)
                text.color = Color.yellow;
            else
                text.color = Color.red;
            time -= pollingTime;
            frameCount = 0;
        }
    }
}