using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
    public int totalScore;
    public int numberCollected = 0;

    public event System.Action AddPoints; // creates an event called OnDeath

    //private DisplayManager displayManager;

    private static ScoreManager scoreManager;

	public static ScoreManager Instance () 
    {
		if (!scoreManager) 
        {
			scoreManager = FindObjectOfType(typeof (ScoreManager)) as ScoreManager;
			if (!scoreManager)
            {
				Debug.LogError ("There needs to be one active ScoreManager script on a GameObject in your scene.");
			}
		}
		return scoreManager;
	}

    private void Awake()
    {
        if (scoreManager == null)
        {
            DontDestroyOnLoad(this.gameObject);
            scoreManager = this;
        }
        else if (scoreManager != null)
        {
            Destroy(gameObject);
        }

        //displayManager = DisplayManager.Instance();
    }

    public void AddScore(int score)
    {
        numberCollected += 1;
        totalScore += score;
       // Debug.Log("numberCollected " + numberCollected);
        AddPoints();
        //if (displayManager != null)
            //displayManager.DisplayScore(totalScore.ToString());
    }

    public void ResetScore()
    {
        totalScore = 0;
        numberCollected = 0;
        //Debug.Log("uhhhhhhhhhhhhhhhhhhhhhhhhhh");
    }
}
