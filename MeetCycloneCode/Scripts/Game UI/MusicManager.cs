using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
	
    [HideInInspector]
	public AudioSource audioSource;
    [HideInInspector]
	public float originalVolume;
    [HideInInspector]
    public float originalVolume2;

	public static MusicManager musicManager;

	public static MusicManager Instance () {
		if (!musicManager) {
			musicManager = FindObjectOfType(typeof (MusicManager)) as MusicManager;
			if (!musicManager){
				Debug.LogError ("There needs to be one active MusicManager script on a GameObject in your scene.");
			}
		}
		return musicManager;
	}

	void Awake(){
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Music Manager");
		if(objs.Length > 1)
			Destroy(this.gameObject);

		DontDestroyOnLoad(gameObject);
		//Debug.Log ("Don't destroy on load: " + name);
	}
	
	void Start (){
		audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume; // this is used in SoundManager in MusicOff()

		SceneManager.sceneLoaded += OnSceneLoaded; // subscribe to the event
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        // play the audioclip of the scene you are on
        AudioClip thisLevelMusic = levelMusicChangeArray[scene.buildIndex];
		
		//Debug.Log ("Playing clip: " + thisLevelMusic);

        if(audioSource.volume <= 0.0f)
        {
            if(scene.buildIndex > 2){
                audioSource.Stop(); // Stop to make sure the next level doesn't just start the song randomly in the middle
                audioSource.Play();
            }
            audioSource.volume = originalVolume2; // To reset the volume to what it was before the fade
        }

        if (thisLevelMusic && audioSource.clip != thisLevelMusic){ //if there is music attached for that scene/level and it is not already playing
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            if (scene.buildIndex > 1) {
                audioSource.PlayDelayed(2f);
            } else {
                audioSource.Play();
            }
		}
	}

	public void SetVolume(float volume){
		audioSource.volume = volume;
	}

     public void CaptureVolume(){
        originalVolume2 = audioSource.volume; // this is for after any fades or stopping of music using volume
    } 

    public void FadeMusicOut(float fadeTime)
    {
        if (audioSource.volume > 0.0f)
        {
            audioSource.volume -= Time.deltaTime / fadeTime;
        }
    }

    public void StopMusicAltogether()
    {
        audioSource.volume = 0.0f;
    }
}
