using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationConstraint : MonoBehaviour
{

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }


    void Update()
    {
        Vector3 currentEulerAngle = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentEulerAngle.y, 0);
    }
}
