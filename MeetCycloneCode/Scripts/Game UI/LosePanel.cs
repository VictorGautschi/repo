using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LosePanel : MonoBehaviour {

	public Text loseText;
	public Button restartLevelButton;
	public Button quitToMainMenuButton;
    public GameObject losePanelObject;

    private Animator anim;

	public static LosePanel losePanel;

	public static LosePanel Instance () {
		if (!losePanel) {
			losePanel = FindObjectOfType(typeof (LosePanel)) as LosePanel;
			if (!losePanel){
				Debug.LogError ("There needs to be one active LosePanel script on a GameObject in your scene.");
			}
		}
		return losePanel;
	}

    public void LoseChoice (string loseText, UnityAction restartEvent, UnityAction quitEvent) {
		losePanelObject.SetActive (true);

        anim = losePanelObject.GetComponent<Animator>();
        anim.SetTrigger("isMoving");

		restartLevelButton.onClick.RemoveAllListeners(); // clear all past events linked to this button
		restartLevelButton.onClick.AddListener (restartEvent);
		restartLevelButton.onClick.AddListener (ClosePanel);

		quitToMainMenuButton.onClick.RemoveAllListeners(); 
		quitToMainMenuButton.onClick.AddListener (quitEvent);
		quitToMainMenuButton.onClick.AddListener (ClosePanel);

		this.loseText.text = loseText;

		restartLevelButton.gameObject.SetActive (true);
		quitToMainMenuButton.gameObject.SetActive (true);
	}	

	void ClosePanel(){
		losePanelObject.SetActive (false);
	}
}
