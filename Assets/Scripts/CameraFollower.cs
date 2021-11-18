using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]Transform target;

    private void OnEnable()
    {
        DrawLine.newBodyCreated += DrawnBody_newBodyCreated;
    }

    private void OnDisable()
    {
        DrawLine.newBodyCreated -= DrawnBody_newBodyCreated;
    }

    private void DrawnBody_newBodyCreated(DrawnBody NewDrawnBody)
    {
        target = NewDrawnBody.transform;
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (target != null)
        {
            Vector3 temp = new Vector3(target.transform.position.x, target.transform.position.y + 3, transform.position.z);
            transform.position = temp;
        }

    }
}
