using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// // This enemy attacks the ship and the player. Can attack multiple times and will do so until killed.

public class Enemy1B : Enemy {

	[Header("Enemy1B Specifics")]
	[Tooltip("The amount of energy that is drained from the player when he is hit with a combat attack. The ranged enemy drain amount for the projectile is set on the projectile.")]
	public float energyDrainPerc = 0.1f;
	[Tooltip("The time from the start of the attack animation until contact has been made with the target")]
	public float drainWaitTimeAnimation = 1.2f;
	// public ParticleSystem energyDrainEffect; // removed and animation added instead
	public ParticleSystem hitShipEffect;

	protected override void Awake(){
		base.Awake();
		anim = GetComponentInChildren<Animator>();
	}

	protected override void Start () { 
		currentHealth = startingHealth;

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
			StartCoroutine(AttackRoutine());

			if(isBoss)
				StartCoroutine(RegenerateHealthOverTime());
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
	}

	protected virtual IEnumerator AttackRoutine(){
		while(hasTarget) { 

			sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

			if(sqrDstToTarget <= Mathf.Pow(attackDistanceThreshold,2)){

				// Must stop pathfollowing here so that enemy does not keep following the path until nextAttackTime
				StopCoroutine("FollowPath"); 
				if (Time.time >= nextAttackTime) {
					nextAttackTime = Time.time + timeBetweenAttacks;
					yield return StartCoroutine(Attack());
				} else {
					yield return null;
				}
			} else {
				anim.SetTrigger("isChasing");
				anim.ResetTrigger("isAttacking");
			}
			yield return null;
		} 
		yield return null;
	}

	protected override IEnumerator Attack() {
		anim.SetTrigger("isAttacking");
		anim.ResetTrigger("isChasing");
		anim.ResetTrigger("isSpawning");

		if (target != null) {
			Vector3 dirToTarget = (target.position - transform.position).normalized;

			if (target == player.transform){
				// This is to allow the animation to play to the point of connecting with the player
				yield return new WaitForSeconds (drainWaitTimeAnimation);

				// If the player moves before the enemy connects, then no energy is drained
				if(sqrDstToTarget <= Mathf.Pow(attackDistanceThreshold,2)) {
					player.EnergyDrain(energyDrainPerc);
                    //Destroy(Instantiate(energyDrainEffect.gameObject,target.transform.position, Quaternion.FromToRotation(Vector3.forward, dirToTarget)) as GameObject, deathEffect.main.startLifetimeMultiplier);
				} 
			} 

			if (target == ship.transform) {
				// This is to allow the animation to play to the point of connecting with the ship
				yield return new WaitForSeconds (drainWaitTimeAnimation);

				if(sqrDstToTarget <= Mathf.Pow((attackDistanceThreshold),2)) {
					ship.TakeDamage(damagePerc);
                    Destroy(Instantiate(hitShipEffect.gameObject,new Vector3(target.transform.position.x,target.transform.position.y + 0.2f, target.transform.position.z), Quaternion.FromToRotation(Vector3.forward, dirToTarget)) as GameObject, hitShipEffect.main.startLifetimeMultiplier);
				}		
			} 
		}
		yield return null;
	}
} 