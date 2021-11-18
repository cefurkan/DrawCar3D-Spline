using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    CheckPointManager checkPointManager;

    private void Awake()
    {
        if (!checkPointManager)
        {
            if (GetComponentInParent<CheckPointManager>())
            {
                checkPointManager = GetComponentInParent<CheckPointManager>();
            }
            else
            {
                checkPointManager = FindObjectOfType<CheckPointManager>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<DrawnBody>())
        {
            checkPointManager.SetActiveCheckPoint(this.transform);
        }
    }
}
