using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script to handle how the balancing platform works
//For now it will just clamp the rotation of the X axis so that it doesn't fall with the player.
public class BalancingPlatform : MonoBehaviour
{
    private Transform platformTransform;
    private Rigidbody rigid;
    [Header("Platform Variables")]
    [SerializeField] private float minRotationValue;
    [SerializeField] private float maxRotationValue;

    [SerializeField] private bool activatePlatform;

    private bool left;
    private bool right;

    float time;
    float rotationValue = 0;

    [SerializeField] private float rotationDuration;
    void Start()
    {
        left = true;
        platformTransform = this.transform;
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activatePlatform)
        {
            RotatePlatform();
        }
    }

    public void RotatePlatform()
    {
        if (left)
        {
            rotationValue -= rotationDuration * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotationValue, transform.rotation.y, transform.rotation.z);

            if(rotationValue <= minRotationValue)
            {
                left = false;
                right = true;
            }

        }

        if (right)
        {
            rotationValue += rotationDuration * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotationValue, transform.rotation.y, transform.rotation.z);

            if (rotationValue >= maxRotationValue)
            {
                left = true;
                right = false;
            }
        }
    }
}
