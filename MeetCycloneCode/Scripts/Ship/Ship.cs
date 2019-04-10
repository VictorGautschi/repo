using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.UI;

public class Ship : LivingEntity {

	private float lastDamageTime;
    private ModalManager modalManager;
    private MusicManager musicManager;
    private SoundEffectsManager soundEffectsManager;

    public AudioClip loseLevelAudio;
    public AudioClip takeDamageAudio;

	[HideInInspector]
	public float fullHealth;

	[HideInInspector]
	public float fullShield;

	[SerializeField]
	private float shieldRegenerateTimeGap;

	[SerializeField]
	private float shieldRegenerateRatePerc;

    public RegularProgressBar health;
	public RegularProgressBar shield;

    public ParticleSystem shipSmokeEffect;

	private void Awake () {
		fullHealth = startingHealth/startingHealth; // to make sure it is always 1 (100%)
		fullShield = 1f;
		modalManager = ModalManager.Instance();
    }

	protected override void Start () {
        musicManager = MusicManager.Instance();
        soundEffectsManager = SoundEffectsManager.Instance();

        health.value = fullHealth;
        StartCoroutine(RegenerateShieldOverTime());
		this.OnDeath += OnShipDeath;
    }

	public override void TakeDamage(float _damage) {
        soundEffectsManager.audioSource.PlayOneShot(takeDamageAudio, soundEffectsManager.audioSource.volume);
        if (shield.value <= 0){
			health.value -= _damage;
            if (health.value < 0)
                health.value = 0;
		}

		if (health.value <= 0 && !dead) { // not already dead
			DontDestroy(); // from LivingEntity
            shipSmokeEffect.Play();
		}

		shield.value -= _damage;
        if (shield.value < 0)
            shield.value = 0;
        lastDamageTime = Time.time;
	}

	IEnumerator RegenerateShieldOverTime() {
        while(health.value > 0f){
			if(Time.time >= (lastDamageTime + shieldRegenerateTimeGap) && shield.value < fullShield){
				shield.value += shieldRegenerateRatePerc;
				yield return new WaitForSeconds(2f);
			} else {
				yield return null;
			}
		}
	}

	void OnShipDeath() {
        // When the level is lost
        musicManager.CaptureVolume();
        musicManager.StopMusicAltogether();
        soundEffectsManager.audioSource.PlayOneShot(loseLevelAudio, soundEffectsManager.audioSource.volume);

        modalManager.RestartQuit();
	}
}