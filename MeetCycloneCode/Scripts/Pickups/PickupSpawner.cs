using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {

	public float startPickupTime = 1f;

	public Pickup[] pickups;
	Pickup currentPickup;

	int currentPickupNumber;
	int pickupsRemainingToSpawn;
	int pickupsRemaining;

	float nextPickupTime;
	float nextSpawnTime;

	void Start () {
		NextPickup(); // Wave 1
		pickupsRemaining = pickups.Length;
		nextPickupTime = Time.time + currentPickup.timeToNextPickup;
        startPickupTime += Time.time;
	}

	void Update(){
		if(Time.time > startPickupTime && pickupsRemaining > 0){

			if(Time.time > nextSpawnTime){
                Instantiate(currentPickup.pickup, transform.position, transform.rotation);
				nextSpawnTime = Time.time + currentPickup.timeToNextPickup;
			}

			if(pickupsRemaining > 0 && Time.time > nextPickupTime){
				pickupsRemaining--;
				nextPickupTime = Time.time + currentPickup.timeToNextPickup;
				NextPickup();
			}
		}

        /* if(pickupsRemaining == 0){
            Destroy(gameObject);
        } */
	}

	void NextPickup () {
		currentPickupNumber ++;

		if (currentPickupNumber - 1 < pickups.Length) {
			currentPickup = pickups[currentPickupNumber - 1];
		}
	}	

	[System.Serializable]
	public class Pickup {
		public GameObject pickup; 
		public float timeToNextPickup;
	}
}
