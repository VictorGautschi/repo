﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCapsule : MonoBehaviour {

	private PickupManager pickupManager;

	public float timeTillDestroy;

	void Start() {
		pickupManager = PickupManager.Instance();
		timeTillDestroy = Time.time + timeTillDestroy;
	}

	void Update(){
		if(Time.time > timeTillDestroy){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.GetComponent<Collider>().tag == "Player"){
			pickupManager.CollectEnergyCapsule();
			Destroy(this.gameObject);
		}
	}
}