using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public delegate void LevelCompleted();
    public static event LevelCompleted levelCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<DrawnBody>())
        {
            levelCompleted?.Invoke();
        }
    }
}
