using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameInfo : MonoBehaviour {

	public float delayShowTime;
	public GameObject toSetActiveTrue;
	public AudioClip infoShowsUpAudio;

	float timer;

	private SoundEffectsManager soundEffectsManager;

	void Awake(){
		soundEffectsManager = SoundEffectsManager.Instance();
		StartCoroutine(ExecuteAfterTime(delayShowTime));
	}

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        SetActiveTrue();
        soundEffectsManager.audioSource.PlayOneShot(infoShowsUpAudio, soundEffectsManager.audioSource.volume);
    }

	private void SetActiveTrue(){
		toSetActiveTrue.gameObject.SetActive(true);
        Time.timeScale = 0;
	}
}
