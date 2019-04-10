using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuPanel : MonoBehaviour {

	public Text question;
	public Button restartLevelButton;
	public Button quitToMainMenuButton;
	public Button cancelButton;
	public GameObject menuPanelObject;

	public static MenuPanel menuPanel;

	public static MenuPanel Instance () {
		if (!menuPanel) {
			menuPanel = FindObjectOfType(typeof (MenuPanel)) as MenuPanel;
			if (!menuPanel){
				Debug.LogError ("There needs to be one active MenuPanel script on a GameObject in your scene.");
			}
		}
		return menuPanel;
	}

	public void MenuChoice (string question, UnityAction restartEvent, UnityAction quitEvent, UnityAction cancelEvent) {
		menuPanelObject.SetActive (true);

		restartLevelButton.onClick.RemoveAllListeners(); // clear all past events linked to this button
		restartLevelButton.onClick.AddListener (restartEvent);
		restartLevelButton.onClick.AddListener (ClosePanel);

		quitToMainMenuButton.onClick.RemoveAllListeners(); 
		quitToMainMenuButton.onClick.AddListener (quitEvent);
		quitToMainMenuButton.onClick.AddListener (ClosePanel);

		cancelButton.onClick.RemoveAllListeners(); 
		cancelButton.onClick.AddListener (cancelEvent);
		cancelButton.onClick.AddListener (ClosePanel);

		this.question.text = question;

		restartLevelButton.gameObject.SetActive (true);
		quitToMainMenuButton.gameObject.SetActive (true);
		cancelButton.gameObject.SetActive (true);
	}	

	void ClosePanel(){
		menuPanelObject.SetActive (false);
	}
}
