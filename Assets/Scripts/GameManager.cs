using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    DrawnBody playerBody;
    CheckPointManager checkPointManager;

    private void Awake()
    {
        checkPointManager = FindObjectOfType<CheckPointManager>();
    }

    private void OnEnable()
    {
        FinishPoint.levelCompleted += FinishPoint_levelCompleted;
        DrawLine.newBodyCreated += DrawnBody_newBodyCreated;
    }

    private void OnDisable()
    {
        FinishPoint.levelCompleted -= FinishPoint_levelCompleted;
        DrawLine.newBodyCreated -= DrawnBody_newBodyCreated;
    }

    private void DrawnBody_newBodyCreated(DrawnBody NewDrawnBody)
    {
        playerBody = NewDrawnBody;
    }

    private void FinishPoint_levelCompleted()
    {
        RestartGame();
    }

    public void RestartGame()
    {
        playerBody.ReturnCheckPoint(checkPointManager.GetStartPoint());

        // SceneManager.LoadScene(0);
    }
}
