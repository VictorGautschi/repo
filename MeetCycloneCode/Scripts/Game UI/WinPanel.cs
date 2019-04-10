using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WinPanel : MonoBehaviour {

	public Text winText;
	public Button restartLevelButton;
	public Button quitToMainMenuButton;
	public Button nextLevelButton;
	public GameObject winPanelObject;

    private Animator anim;

	public static WinPanel winPanel;

	public static WinPanel Instance () {
		if (!winPanel) {
			winPanel = FindObjectOfType(typeof (WinPanel)) as WinPanel;
			if (!winPanel){
				Debug.LogError ("There needs to be one active WinPanel script on a GameObject in your scene.");
			}
		}
		return winPanel;
	}

	public void WinChoice (string question, UnityAction restartEvent, UnityAction quitEvent, UnityAction NextLevelEvent) {
        winPanelObject.SetActive (true);

        anim = winPanelObject.GetComponent<Animator>();
        anim.SetTrigger("isMoving");

        restartLevelButton.onClick.RemoveAllListeners(); // clear all past events linked to this button
		restartLevelButton.onClick.AddListener (restartEvent);
		restartLevelButton.onClick.AddListener (ClosePanel);

		quitToMainMenuButton.onClick.RemoveAllListeners(); 
		quitToMainMenuButton.onClick.AddListener (quitEvent);
		quitToMainMenuButton.onClick.AddListener (ClosePanel);

		nextLevelButton.onClick.RemoveAllListeners(); 
		nextLevelButton.onClick.AddListener (NextLevelEvent);
		nextLevelButton.onClick.AddListener (ClosePanel);

		this.winText.text = question;

		restartLevelButton.gameObject.SetActive (true);
		quitToMainMenuButton.gameObject.SetActive (true);
		nextLevelButton.gameObject.SetActive (true);
    }

    void ClosePanel(){
		winPanelObject.SetActive (false);
	}
}
