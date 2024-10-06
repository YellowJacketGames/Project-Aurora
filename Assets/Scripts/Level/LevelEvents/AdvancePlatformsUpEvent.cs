using System;
using System.Collections.Generic;
using UnityEngine;

public class AdvancePlatformsUpEvent : LevelEvent
{
    [SerializeField] private List<GameObject> elementsFront;
    [SerializeField] private List<GameObject> elementsBack;
    [SerializeField] private List<GameObject> elementsRight;
    [SerializeField] private List<GameObject> elementsLeft;
    [SerializeField] private float heightIncrement;
    [SerializeField] private int maxReps = 12;
    [SerializeField] private int currentReps;
    private int _index = 1;
    // 1-> trigger in elementsBack, related to elementsFront. 2-> trigger elementsLeft, related to elementsRight.
    // 3-> triggerelementsFront, related to elements Back. 4-> trigger elementsRight, related to elementsLeft. 
    public override void OnEvent()
    {
        if(currentReps+1>maxReps) return;
        Debug.Log("Triggered");
        // base.OnEvent();
        switch (_index)
        {
            case 1:
                foreach (var e in elementsFront)
                    IncrementElementHeight(e);
                break;
            case 2:
                foreach (var e in elementsRight)
                    IncrementElementHeight(e);
                break;
            case 3:
                foreach (var e in elementsBack)
                    IncrementElementHeight(e);
                break;
            case 4:
                foreach (var e in elementsLeft)
                    IncrementElementHeight(e);
                break;
        }

        _index++;
        if (_index > 4)
            _index = 1;
    }

    private void IncrementElementHeight(GameObject e)
    {
        var position = e.transform.position;
        position = new Vector3(position.x, position.y + heightIncrement, position.z);
        e.transform.position = position;
        currentReps++;
    }
}