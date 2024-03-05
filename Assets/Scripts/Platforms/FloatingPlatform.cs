using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    [SerializeField] private float floatingSpeed;
    [SerializeField] private float floatingLength;

    [SerializeField] private float xTest;
    [SerializeField] private float xTest2;


    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * floatingSpeed, floatingLength), transform.position.z);

        transform.localEulerAngles = new Vector3(Mathf.PingPong(Time.time * xTest, xTest2), 0, 0);

    }
}
