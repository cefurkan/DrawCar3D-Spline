using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] bool isRotating;
    [SerializeField] float xAngle, yAngle, zAngle;

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        }
    }
}
