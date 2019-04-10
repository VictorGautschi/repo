using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dreamteck.UI;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
	public Scrollbar difficultyScrollBar;
	public SwitchToggle musicSwitchToggle;

	private MusicManager musicManager;
	
	// Use this for initialization
	void Start () {
		if (musicManager != null)
			musicManager = MusicManager.Instance();
		//Debug.Log (musicManager);
		
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		difficultyScrollBar.value = PlayerPrefsManager.GetDifficulty();
		musicSwitchToggle.state = PlayerPrefsManager.GetMusic();
	}
	
	// Update is called once per frame
	void Update () {
		if (musicManager != null)
			musicManager.SetVolume(volumeSlider.value);

		SettingsValues();
	}
	
	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		PlayerPrefsManager.SetDifficulty(difficultyScrollBar.value);
		PlayerPrefsManager.SetMusic(musicSwitchToggle.state);
	}

	public void SetDefaults(){
		volumeSlider.value = 0.8f;
		difficultyScrollBar.value = 0f;
		musicSwitchToggle.state = true;
	}

	void SettingsValues() {
		GameObject go1 = difficultyScrollBar.gameObject.transform.parent.GetChild(2).gameObject;
		if(difficultyScrollBar.value <= 0.25) {
			go1.GetComponent<Text>().text = "NORMAL";
		} else if(difficultyScrollBar.value > 0.25 && difficultyScrollBar.value < 0.75) {
			go1.GetComponent<Text>().text = "HARD";
		} else {
			go1.GetComponent<Text>().text = "INSANE";
		}

		GameObject go2 = volumeSlider.gameObject.transform.parent.GetChild(2).gameObject;
		go2.GetComponent<Text>().text = (Mathf.RoundToInt(volumeSlider.value * 100)).ToString();

		GameObject go3 = musicSwitchToggle.gameObject.transform.parent.GetChild(2).gameObject;
		if(musicSwitchToggle.state == false){
			go3.GetComponent<Text>().text = "OFF";
		} else {
			go3.GetComponent<Text>().text = "ON";
		}
	}
}
