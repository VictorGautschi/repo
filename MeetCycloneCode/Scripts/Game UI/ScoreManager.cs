using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Dreamteck.UI;

public class ScoreManager : MonoBehaviour {

	//int levelScore;
	//int totalPossibleScore;
	[HideInInspector]
	public int numberStars;
	//public Text scoreText;
	//public Text totalPossibleScoreText;
	public ProgressGroup totalStarsProgressGroup;

    private Ship ship;

	private static ScoreManager scoreManager;

	public static ScoreManager Instance () {
		if (!scoreManager) {
			scoreManager = FindObjectOfType(typeof (ScoreManager)) as ScoreManager;
			if (!scoreManager){
				Debug.LogError ("There needs to be one active ScoreManager script on a GameObject in your scene.");
			}
		}
		return scoreManager;
	}

	//void Awake(){
	//	DisplayScore();
	//}

    private void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
    }

 //   public void AddScore(int _levelScore){
	//	levelScore += _levelScore;
	//	//DisplayScore();
	//}

	//public void SubtractScore(int _levelScore){
	//	levelScore -= _levelScore;
	//	if(levelScore < 0)
	//		levelScore = 0;
	//	//DisplayScore();
	//}

	//public void AddTotalPossibleScore(int _totalPossibleScore){
	//	totalPossibleScore += _totalPossibleScore;
	//	totalPossibleScoreText.text = "Possible: " + totalPossibleScore.ToString();
	//}

	public void DisplayEndScore(){
        if(ship.health.value < 0.5f){
            numberStars = 1;
        } else if(ship.health.value < 0.9f && ship.health.value >= 0.5f){
            numberStars = 2;
        } else {
            numberStars = 3;
        }

		StoreStarsPerLevel(SceneManager.GetActiveScene().buildIndex - 2,numberStars);
		totalStarsProgressGroup.progress = numberStars;
	}

	//void DisplayScore(){
	//	scoreText.text = "Score: " + levelScore.ToString();
	//}

	void StoreStarsPerLevel(int _level, int _numberStars) {
        if (_numberStars > GameControl.control.numberStarsPerLevel[_level - 1])
        {
            GameControl.control.numberStarsPerLevel[_level - 1] = _numberStars; //To save to file
        }
	}
}
