using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is a parent class so that it includes the 'parent' reference and the same awake method that
//all player components should include.
public class PlayerComponent : MonoBehaviour
{
    protected PlayerController _parent;
    public virtual void Awake()
    {
        //We use try get component so that if the component is missing it doesn't crash the game.
        if (TryGetComponent<PlayerController>(out PlayerController p))
        {
            _parent = p;
        }

        else
        {
            //Warning for knowing what the issue is in the console.
            Debug.LogWarning("Player Controller component is missing in the gameobject");
        }
    }
}
