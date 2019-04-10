using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditPickup : MonoBehaviour {
    
	[SerializeField]
	private int creditAmount;
	private CreditManager creditManager;

	public float timeTillDestroy;

    private SoundEffectsManager soundEffectsManager;
    public AudioClip creditPickupAudio;

	void Awake () {
		creditManager = CreditManager.Instance();
		timeTillDestroy = Time.time + timeTillDestroy;
	}

    void Start()
    {
        soundEffectsManager = SoundEffectsManager.Instance();       
    }

    void Update(){
		if(Time.time > timeTillDestroy){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.GetComponent<Collider>().tag == "Player") {
			creditManager.AddCredit(creditAmount);
            soundEffectsManager.audioSource.PlayOneShot(creditPickupAudio, soundEffectsManager.audioSource.volume);
			Destroy(this.gameObject);
		}
	}
}
