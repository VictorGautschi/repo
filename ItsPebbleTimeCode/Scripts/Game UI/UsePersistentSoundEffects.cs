using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePersistentSoundEffects : MonoBehaviour {

    SoundEffectsManager soundEffectsManager;

    public AudioClip buttonSoundClip;

	void Awake () {
        soundEffectsManager = SoundEffectsManager.Instance();
	}
	
	public void ButtonSound () {
        soundEffectsManager.audioSource.PlayOneShot(buttonSoundClip);
	}
}
