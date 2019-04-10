using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour {

	float turnSpeed = 10f;

	Ship ship;
	Player player;
	Enemy enemy;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
		enemy = GetComponentInParent<Enemy>();
	}
	
	void Update () {
		if (ship != null) {
			if(enemy.target == ship.transform) {
				Vector3 relativePos = ship.transform.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativePos);
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
			} 

			if(enemy.target == player.transform) {
				Vector3 relativePos = player.transform.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativePos);
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
			}
		}
	}
}
