using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    CheckPointManager checkPointManager;

    private void Awake()
    {
        if (checkPointManager == null)
        {
            checkPointManager = FindObjectOfType<CheckPointManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<DrawnBody>())
        {
            other.GetComponentInParent<DrawnBody>().ReturnCheckPoint(checkPointManager.GetActiveCheckPoint());
        }

    }
}
