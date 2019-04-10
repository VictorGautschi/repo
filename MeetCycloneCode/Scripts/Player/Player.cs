using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.UI;

public class Player : MonoBehaviour, IDamageable
{
    protected float startingEnergy = 1f;
    protected float currentEnergy;

    [HideInInspector]
    public float fullEnergy;

    [HideInInspector]
    public bool weaponActive;

    [HideInInspector]
    public bool isInShip;

	public float regenerateTimeGap;
	public float regenerateRatePerc;

	public ParticleSystem rechargeEffect;

	public RegularProgressBar energy;
	private GameObject ship;

    private Animator anim;
    private Animator shipAnim;

    private SoundEffectsManager soundEffectsManager;
    private DisplayManager displayManager;
    public AudioClip shipOpenAudio;
    public AudioClip shipCloseAudio;
    public AudioClip takeDamageAudio;

    private IEnumerator chargeCyclone;

	private void Awake () {
		ship = GameObject.FindGameObjectWithTag("Ship");
		fullEnergy = startingEnergy/startingEnergy; // Assuming that cyclone will start the level with full energy
		energy.value = fullEnergy; // initializing the value of the bar to full
        displayManager = DisplayManager.Instance();
    }

	void Start () {
		chargeCyclone = RegenerateEnergyOverTime();
        shipAnim = ship.GetComponentInChildren<Animator>();
        anim = GetComponentInChildren<Animator>();
        soundEffectsManager = SoundEffectsManager.Instance();
    }

	protected IEnumerator RegenerateEnergyOverTime() {
		while(true){
			
			if (ship != null){
				if(energy.value < fullEnergy){
                    yield return new WaitForSeconds(regenerateTimeGap);
					energy.value += regenerateRatePerc;
					rechargeEffect.Play();
				} else {
					yield return null;
				}
			}
			yield return null;
		}
	}

	public void EnergyDrain(float _energy) { 
        soundEffectsManager.audioSource.PlayOneShot(takeDamageAudio, soundEffectsManager.audioSource.volume);
        if (energy.value > 0){
			energy.value -= _energy;
            if (energy.value < 0)
                energy.value = 0;
            anim.ResetTrigger("isScared");
            anim.ResetTrigger("isNormal");
            anim.SetTrigger("isAngry");
        }
	}

	void OnTriggerEnter(Collider _ship) {
        if(_ship == ship.GetComponent<Collider>()) { 
			//Debug.Log("In " + Time.time);
			StartCoroutine(chargeCyclone);
            shipAnim.Play("ship_open_cockpit");
            soundEffectsManager.audioSource.PlayOneShot(shipOpenAudio, soundEffectsManager.audioSource.volume);
            isInShip = true;    
            weaponActive = false;
            displayManager.DisplayMessage("Weapons Disabled");
        }
	}

	void OnTriggerExit(Collider _ship) {
		if(_ship == ship.GetComponent<Collider>()) {
			//Debug.Log("Out " + Time.time);
			StopCoroutine(chargeCyclone);
			rechargeEffect.Stop();
            shipAnim.Play("ship_close_cockpit");
            soundEffectsManager.audioSource.PlayOneShot(shipCloseAudio, soundEffectsManager.audioSource.volume);
            isInShip = false;
            weaponActive = true;
            displayManager.DisplayMessage("Weapons Enabled");
        }
	}

	public void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDirection) {
		// Do some stuff here with _hit variable
		TakeDamage(_damage);
    }

	public void TakeDamage(float _damage) { 
        soundEffectsManager.audioSource.PlayOneShot(takeDamageAudio, soundEffectsManager.audioSource.volume);
		//EnergyDrain(_damage);
		if (energy.value > 0){
			energy.value -= _damage;
            if (energy.value < 0)
                energy.value = 0;
            anim.ResetTrigger("isScared");
            anim.ResetTrigger("isNormal");
            anim.SetTrigger("isAngry"); 
        }
	}
}
