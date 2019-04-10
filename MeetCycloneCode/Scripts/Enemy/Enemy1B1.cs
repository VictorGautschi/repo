using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This enemy spawns babies and attacks both the ship and the player.

public class Enemy1B1 : Enemy1B {

	[Header("Enemy1B1 Specifics")]
	public Enemy babyEnemy;
	[Tooltip("the number of babies grouped together in a wave")]
	public int numberBabiesInWave;
	[Tooltip("the time between groups of babies")]
	public float timeBetweenSpawns;
	[Tooltip("the time between individual babies")]
	public float timeBetweenBabies;
	[Tooltip("When cyclone gets this close to the enemy, the enemy will not spawn babies")]
	public float stopSpawnDistanceToCyclone;

	private float nextSpawnTime;

	protected override IEnumerator AttackRoutine(){
		timeBetweenBabies = timeBetweenBabies/2;

		yield return new WaitForSeconds (timeBetweenSpawns);

		while(hasTarget) { 

			sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

			if(sqrDstToTarget <= Mathf.Pow(stopSpawnDistanceToCyclone,2)){
				anim.SetTrigger("isChasing");
				anim.ResetTrigger("isSpawning");
				anim.ResetTrigger("isAttacking");

				if(sqrDstToTarget <= Mathf.Pow(attackDistanceThreshold,2)){

					// Must stop pathfollowing here so that enemy does not keep following the path until nextAttackTime
					StopCoroutine("FollowPath"); 
					if (Time.time >= nextAttackTime) {
						nextAttackTime = Time.time + timeBetweenAttacks;
						yield return StartCoroutine(Attack());
					}
				}
			} else {
				if(Time.time >= nextSpawnTime){
					nextSpawnTime = Time.time + timeBetweenSpawns;
					//StopCoroutine("FollowPath"); 
					anim.SetTrigger("isSpawning");
					anim.ResetTrigger("isChasing");
					anim.ResetTrigger("isAttacking");
					for (int i = 0; i < numberBabiesInWave; i++) {
						yield return new WaitForSeconds (timeBetweenBabies);
                        babyEnemy.isBabyEnemy = true;
						Instantiate(babyEnemy,this.transform.position,transform.rotation);
                        // NOTE: We can't make the numberDeadEnemies change here so we have to remove this from the count.
                        //enemyCountManager.sumEnemies += 1;
						yield return new WaitForSeconds (timeBetweenBabies);
					}
					//StartCoroutine("FollowPath");
				} else {
					anim.SetTrigger("isChasing");
					anim.ResetTrigger("isSpawning");
					anim.ResetTrigger("isAttacking");
				}
			}
			yield return null;
		} 
		yield return null;
	}
}