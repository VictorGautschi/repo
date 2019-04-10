using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour 
{
    ScoreManager scoreManager;
    TimeManager timeManager;
    Ball ball;
    int numberCollectablesCollected;

    public float reduceTimePercentage = 0.01f;
    public float minimumTime = 3.0f;
    public int scoreMoreDifficultInt = 2;
    public float speedPercentageIncrease = 0.01f;
    public float maximumSpeed = 5.0f;


    private static DifficultyManager difficultyManager;

    public static DifficultyManager Instance()
    {
        if (!difficultyManager)
        {
            difficultyManager = FindObjectOfType(typeof(DifficultyManager)) as DifficultyManager;
            if (!difficultyManager)
            {
                Debug.LogError("There needs to be one active DifficultyManager script on a GameObject in your scene.");
            }
        }
        return difficultyManager;
    }

    private void Awake()
    {
        scoreManager = ScoreManager.Instance();
        timeManager = TimeManager.Instance();
        ball = Ball.Instance();

        numberCollectablesCollected = 0;
    }

    private void Update()
    {
        if(scoreManager.numberCollected >= numberCollectablesCollected + scoreMoreDifficultInt)
        {

            //Debug.Log("Yeah -------------===================");

            ReduceTotalTime();
            IncreaseBallSpeed();
            numberCollectablesCollected = scoreManager.numberCollected;

            // Idea - to stretch the game out longer
            //scoreMoreDifficultInt += 1;
        }
    }

    void ReduceTotalTime()
    {
        if(timeManager.maxTime > minimumTime)
            timeManager.maxTime -= (timeManager.maxTime * reduceTimePercentage);
    }

    void IncreaseBallSpeed()
    {
        if(ball.maxSpeed < maximumSpeed)
            ball.maxSpeed += (ball.maxSpeed * speedPercentageIncrease);
    }
}
