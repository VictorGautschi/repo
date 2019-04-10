using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimeManager: MonoBehaviour {

	float timeLeft = 0;
	public Text timerText;

//	private ModalManager modalManager;
//	private LevelManager levelManager;

	private float minutes;
	private float seconds;
	private float originalTimeScale;

	private static TimeManager timeManager;

	public static TimeManager Instance () {
		if (!timeManager) {
			timeManager = FindObjectOfType(typeof (TimeManager)) as TimeManager;
			if (!timeManager){
				Debug.LogError ("There needs to be one active TimeManager script on a GameObject in your scene.");
			}
		}
		return timeManager;
	}

//	void Awake () {
//		modalManager = ModalManager.Instance();
//		levelManager = LevelManager.Instance();
//	}

	void Update () {

		timeLeft += Time.deltaTime;
		minutes = Mathf.Floor(timeLeft / 60);
		seconds = timeLeft % 60;
		if (seconds > 59) seconds = 59;
		if (minutes < 0) {
			minutes = 0;
			seconds = 0;
		}

		timerText.text = string.Format("{0:0}:{1:00}",minutes,seconds);

//		if(timeLeft <= 0) {
//			// When the level is won 
//			levelManager.WinLevel();
//			modalManager.RestartQuitNext();
//			Destroy(gameObject); // This is why it is on its own GameObject
//		}
	}
}
