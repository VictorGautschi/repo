using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy {

	public float energyDrainPerc = 0.1f;
	float interpolation;
	public ParticleSystem energyDrainEffect;

	protected override void Start () { 
		currentHealth = startingHealth;
		skinMaterial = GetComponentInChildren<Renderer>().material;
		originalColor = skinMaterial.color;
		attackDistanceThreshold = 2.6f;

		if (player != null){
			currentState = State.Chasing; 
			hasTarget = true;

		 	if (player.energy.value > 0) { 
				target = player.transform; 
				player = target.transform.GetComponent<Player>(); 							
			} else {
				target = ship.transform;
				ship = target.transform.GetComponent<LivingEntity>() as Ship; 
				ship.OnDeath += OnTargetDeath; 
			}
			StartCoroutine(UpdatePath());
		}
	}

	protected override void Update () {
		if (player.energy.value > 0) { 
			if (player != null){
				target = player.transform; 
			}
		} else {
			if (ship != null){
				target = ship.transform; 
			}
		}
		base.Update();
	}

	protected override IEnumerator Attack() {

		currentState = State.Attacking;
		StopCoroutine("FollowPath"); // stop the pathfinding

		Vector3 originalPosition = transform.position;
		Vector3 dirToTarget = (target.position - transform.position).normalized;
		Vector3 attackPosition = target.position - dirToTarget * (1);

		float attackSpeed = 5;
		float percent = 0;

		skinMaterial.color = Color.blue;
		bool hasAppliedDamage = false;
				
		while (percent <= 1) {

			if(percent >= 0.5f && !hasAppliedDamage) { // 0.5 percent is when the enemy is halfway through its lunge, which is when it hits the player. 
				hasAppliedDamage = true;

				if (target != null) {
					if (target == player.transform){
						player.EnergyDrain(energyDrainPerc);
						Destroy(gameObject);
						Destroy(Instantiate(energyDrainEffect.gameObject,target.transform.position, Quaternion.FromToRotation(Vector3.forward, dirToTarget)) as GameObject, deathEffect.main.startLifetimeMultiplier); // changed from deathEffect.startLifetime
					} 

					if (target == ship.transform) {
						ship.TakeDamage(damagePerc);
					}
				}
			}

			percent += Time.deltaTime * attackSpeed;

			if (target == player.transform){
				interpolation = percent;
			} else {
				interpolation = (-Mathf.Pow(percent,2) + percent) * 4; // porabola equation
			}

			transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation); 

			yield return null;	
		} 
		skinMaterial.color = originalColor;
		currentState = State.Chasing;
	}
}