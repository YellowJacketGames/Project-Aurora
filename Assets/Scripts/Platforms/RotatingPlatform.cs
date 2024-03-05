using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Header("Rotating Platform Variables")]
    [Header("X rotation values")]
    [Range(0,50)][SerializeField] private float xRotatingAngle;
    [Range(0, 50)] [SerializeField] private float xRotatingForce;

    [Header("Y rotation values")]
    [Range(0, 50)] [SerializeField] private float yRotatingAngle;
    [Range(0, 50)] [SerializeField] private float yRotatingForce;

    [Header("Z rotation values")]
    [Range(0, 50)] [SerializeField] private float zRotatingAngle;
    [Range(0, 50)] [SerializeField] private float zRotatingForce;
    // Update is called once per frame
    void Update()
    {

        transform.localEulerAngles = new Vector3(Mathf.PingPong(Time.time * xRotatingAngle, xRotatingForce), Mathf.PingPong(Time.time * yRotatingAngle, yRotatingForce), Mathf.PingPong(Time.time * zRotatingAngle, zRotatingForce));
    }
}
