using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagManager : MonoBehaviour
{
    [SerializeField] List<Speaker> gameSpeakers;

    public Speaker ReturnSpeaker(string tag)
    {
        foreach(Speaker s in gameSpeakers)
        {
            if(tag == s.ReturnTag())
            {
                return s;
            }
        }

        Debug.LogError("No speaker was found, maybe the name on the scriptable object or the tag on the ink file isn't correct, please fix this issue");
        return null;
    }
}




