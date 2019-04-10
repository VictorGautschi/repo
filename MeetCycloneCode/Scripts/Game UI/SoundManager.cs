using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

	private MusicManager musicManager;

	[HideInInspector]
	public float currentVolume;

	public static SoundManager soundManager;

	public static SoundManager Instance () {
		if (!soundManager) {
			soundManager = FindObjectOfType(typeof (SoundManager)) as SoundManager;
			if (!soundManager){
				Debug.LogError ("There needs to be one active SoundManager script on a GameObject in your scene.");
			}
		}
		return soundManager;
	}

	void Start () {
		musicManager = MusicManager.Instance();
		MusicOffOn(); // A check to make sure the sound button shows off when off when level is restarted
	}
	
	public void MusicOnOff(){
		if (musicManager.audioSource.volume > 0.0f){
			MusicOff();
		} else {
			MusicOn();
		}
	}

	public void MusicOffOn(){
		if (musicManager != null) {
			currentVolume = musicManager.audioSource.volume;
		} else {
			currentVolume = 0f;
		}

		//Debug.Log("Current Volume " + currentVolume);
		
		if (currentVolume <= 0.0f){
			MusicOff();
			//Debug.Log("Happening");
		} else {
			MusicOn();
		}
	}

	public void MusicOff () {
		if (musicManager != null)
			musicManager.SetVolume(0.0f);
	}

	public void MusicOn(){
		if (musicManager != null)
		 	musicManager.SetVolume(musicManager.originalVolume);
	}	
}
