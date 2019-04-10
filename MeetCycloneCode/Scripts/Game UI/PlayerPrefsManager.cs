using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

	// central control of player prefs
	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string MUSIC_ON_OFF = "music_on_off";
	//const string LEVEL_KEY = "level_unlocked_";
	
	//Volume PlayerPrefs - wrapper
	public static void SetMasterVolume (float volume){
		if(volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Master volume out of range");
		}	
	}
	
	public static float GetMasterVolume () {
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
	
			
	// Difficulty PlayerPrefs - wrapper
	public static void SetDifficulty(float difficulty){
		if(difficulty >= 0f && difficulty <= 1f){
			PlayerPrefs.SetFloat (DIFFICULTY_KEY,difficulty);
		} else {
			Debug.LogError("Trying to set to a difficulty that does not exist");
		}
	}

	public static float GetDifficulty(){
		return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
	}


	// Music On/Off PlayerPrefs - wrapper
	public static void SetMusic(bool musicOnOff){
		PlayerPrefs.SetInt(MUSIC_ON_OFF, musicOnOff ? 1 : 0);
	}

	public static bool GetMusic(){
		int value = PlayerPrefs.GetInt(MUSIC_ON_OFF);

		if (value == 1){
			return true;
		} else {
			return false;
		}
	}
}
