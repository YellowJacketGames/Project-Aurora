using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speaker", menuName = "ScriptableObjects/Create new speaker", order = 1)]
public class Speaker : ScriptableObject //This is a scriptable object class to create as many speakers as we want
{
    [Header("Speaker Variables")]
    [SerializeField] private string speakerTag; //Variable to hold the speaker tag
    [SerializeField] public string speakerName; //Variable to hold the speaker tag

    [SerializeField] public Sprite speakerPortrait; //Variable to hold the speaker portrait, this should later be replaced by an animation profile

    [Tooltip("Variable to hold if the layout goes on the right or the left, 0 for left, 1 for right")]
    [SerializeField] public int layoutOrder; //Variable to hold if the layout goes on the right or the left, 0 for left, 1 for right


    public string ReturnTag()
    {
        return speakerTag;
    }

}
