using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<DrawnBody>())
        {
            other.GetComponentInParent<Rigidbody>().AddForce(Vector3.right * 4000);
        }
    }
}
