using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmPanel : MonoBehaviour {

	public Button confirmButton;
	public GameObject confirmPanelObject;
	public Button unconfirmButton;
	public GameObject unconfirmPanelObject;

    private SoundEffectsManager soundEffectsManager;
    public AudioClip upgradeAudio;

	public static ConfirmPanel confirmPanel;

	public static ConfirmPanel Instance () {
		if (!confirmPanel) {
			confirmPanel = FindObjectOfType(typeof (ConfirmPanel)) as ConfirmPanel;
			if (!confirmPanel){
				Debug.LogError ("There needs to be one active ConfirmUpgradePanel script on a GameObject in your scene.");
			}
		}
		return confirmPanel;
	}

    void Start()
    {
        soundEffectsManager = SoundEffectsManager.Instance();
    }

    public void Confirm (UnityAction _confirmEvent, Transform _placement, bool _useable) {

		if (_useable == true) {
			confirmPanelObject.SetActive (true);
			unconfirmPanelObject.SetActive(true);
			confirmPanelObject.transform.position = _placement.position;

			confirmButton.onClick.RemoveAllListeners(); // clear all past events linked to this button
			confirmButton.onClick.AddListener (_confirmEvent);
			confirmButton.onClick.AddListener (ClosePanel);

			unconfirmButton.onClick.RemoveAllListeners();
			unconfirmButton.onClick.AddListener (ClosePanel);
		} 
	}

	public void Confirm (UnityAction _confirmEvent, Transform _placement) {
		confirmPanelObject.SetActive (true);
		unconfirmPanelObject.SetActive(true);

		confirmPanelObject.transform.position = _placement.position;

		confirmButton.onClick.RemoveAllListeners(); // clear all past events linked to this button
		confirmButton.onClick.AddListener (_confirmEvent);
		confirmButton.onClick.AddListener (ClosePanel);

		unconfirmButton.onClick.RemoveAllListeners();
		unconfirmButton.onClick.AddListener (ClosePanel);

	}

	void ClosePanel(){
		confirmPanelObject.SetActive (false);
		unconfirmPanelObject.SetActive(false);
	}

    public void ConfirmSound(){
        soundEffectsManager.audioSource.PlayOneShot(upgradeAudio, soundEffectsManager.audioSource.volume);
    }
}
