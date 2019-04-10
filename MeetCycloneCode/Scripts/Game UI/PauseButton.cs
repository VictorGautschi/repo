using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseButton : MonoBehaviour {

	public Button pauseButton;
	public GameObject pausedPanel; // stop the player from pathfinding when paused
    public GameObject preGameInfoObject;

	public static PauseButton pausePanel;

	public static PauseButton Instance () {
		if (!pausePanel) {
			pausePanel = FindObjectOfType(typeof (PauseButton)) as PauseButton;
			if (!pausePanel){
				Debug.LogError ("There needs to be one active PauseButton script on a GameObject in your scene.");
			}
		}
		return pausePanel;
	}

    public void Pause () {
		
        if(Time.timeScale >= 1){
			Time.timeScale = 0;
			//pauseButton.GetComponentInChildren<Text>().text = "Play";
			pausedPanel.SetActive(true);
			pauseButton.gameObject.SetActive(false);
			pauseButton.enabled = false;

		} else {
            if (!preGameInfoObject.activeInHierarchy)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
			
			//pauseButton.GetComponentInChildren<Text>().text = "Pause";
			pausedPanel.SetActive(false);
			pauseButton.gameObject.SetActive(true);
			pauseButton.enabled = true;
		}
	}
}
