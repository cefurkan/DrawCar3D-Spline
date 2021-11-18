using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private Transform activeCheckPoint;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform finishPoint;

    private void Awake()
    {
        if(activeCheckPoint == null)
        {
            activeCheckPoint = startPoint;
        }
    }

    public void SetActiveCheckPoint(Transform checkPoint)
    {
        activeCheckPoint = checkPoint;
    }

    public Transform GetActiveCheckPoint()
    { 
        return activeCheckPoint; 
    }

    public Transform GetStartPoint()
    {
        return startPoint;
    }
}
