using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a ranged enemy that attacks the ship only.

public class Enemy1A3 : Enemy1A {

	[Header("Enemy1A3 Specifics")]
	public GameObject fakeProjectile;
	public Transform projectileSpawnPoint;
	[Tooltip("When the enemy gets this close to the ship, it will stop and start throwing spells")]
	public float startShootDistanceToShip;
	[Tooltip("Time between throwing spells (throw animation time), not to be confused with timeBetweenAttacks which is close combat attack")]
	public float timeBetweenSpells;

	private float nextSpellTime;

	GameObject newProjectile;

	WeaponController weaponController;

	protected override void Awake(){
		base.Awake();
		weaponController = GetComponentInChildren<WeaponController>();
	}

	protected override IEnumerator AttackRoutine(){

		yield return new WaitForSeconds (timeBetweenSpells); // Length of the animation

		while(hasTarget){
			sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

			if(newProjectile != fakeProjectile && newProjectile != null)
					Destroy(newProjectile);

			if (sqrDstToTarget <= Mathf.Pow(startShootDistanceToShip,2)){
							
				if (Time.time > nextSpellTime && target != null) {
					nextSpellTime = Time.time + timeBetweenSpells;

					StopCoroutine("FollowPath");
					anim.SetTrigger("isThrowing");
					anim.ResetTrigger("isChasing");
					anim.ResetTrigger("isAttacking");

					// When one is not available then the other is
					if(newProjectile == null){
						newProjectile = Instantiate(fakeProjectile,projectileSpawnPoint.transform.position,Quaternion.identity) as GameObject;
						newProjectile.transform.SetParent(projectileSpawnPoint.transform);
					}					

					yield return new WaitForSeconds (timeBetweenSpells/4);
					Destroy(newProjectile);
					weaponController.Shoot();

					yield return new WaitForSeconds(timeBetweenSpells/4);

					// When one is not available then the other is
					if(newProjectile == null){
						newProjectile = Instantiate(fakeProjectile,projectileSpawnPoint.transform.position,Quaternion.identity) as GameObject;
						newProjectile.transform.SetParent(projectileSpawnPoint.transform);	
					}

					/* NOTE: As long as the total wait time in here is less than the timeBetweenAttacks, 
					   the spell will be cast at the right time. */

				} 
			} 
			yield return null;
		}
	}
}