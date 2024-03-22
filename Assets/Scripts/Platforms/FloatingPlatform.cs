using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    [SerializeField] private float floatingSpeed;
    [SerializeField] private float floatingLength;

    [SerializeField] private float rotationX;
    [SerializeField] private float rotationX2;


    void Update()
    {
        
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time * floatingSpeed, floatingLength), transform.localPosition.z);
        
        if(rotationX > 0 && rotationX2 > 0)
        {
            transform.localEulerAngles = new Vector3(Mathf.PingPong(Time.time * rotationX, rotationX2), 0, 0);
        }

    }
}
