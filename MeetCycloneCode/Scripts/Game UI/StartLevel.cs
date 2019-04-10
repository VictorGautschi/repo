using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartLevel : MonoBehaviour {

	private UnityAction myConfirmLevelAction;
	public string sceneName;

	public Button startButton;
	public GameObject selectedLevelObject;

	private LevelManager levelManager;

	void Awake() {
		levelManager = LevelManager.Instance();
		myConfirmLevelAction = new UnityAction (LoadSelectedLevel);
		startButton.interactable = false; // make sure you didn't apply ot the prefab on this one if you getting an error!
	}

	public void SelectLevel() {
		// what happens when the level button is clicked on
		// it will move the selectedlevelimage to the transform of the level button selected.

		selectedLevelObject.SetActive (true);
		selectedLevelObject.transform.position = this.transform.position;

		startButton.interactable = true;
		startButton.onClick.RemoveAllListeners(); // clear all past events linked to this button
		startButton.onClick.AddListener (myConfirmLevelAction);
		//startButton.onClick.AddListener (ClosePanel);
	}

	public void LoadSelectedLevel() {
		// The function that is called from the Button of the level to load
		levelManager.LoadLevel(sceneName);
	}
}
