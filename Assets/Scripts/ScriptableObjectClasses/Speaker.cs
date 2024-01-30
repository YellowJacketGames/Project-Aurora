using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speaker", menuName = "ScriptableObjects/Create new speaker", order = 1)]
public class Speaker : ScriptableObject //This is a scriptable object class to create as many speakers as we want
{
    [Header("Speaker Variables")]
    [SerializeField] public string speakerName; //Variable to hold the speaker name

    [SerializeField] public Sprite speakerPortraitLeft;   //Variable to hold the speaker portrait, this should later be replaced by an animation profile
    [SerializeField] public Sprite speakerPortraitRight;  //Variable to hold the speaker portrait, this should later be replaced by an animation profile

    [SerializeField] public InteractDirection currentDirection; //variable to check the current speaker direction to adjust the proper portrait



}
