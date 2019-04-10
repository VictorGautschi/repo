using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This enemy spawns babies and attacks the ship only.

public class Enemy1A1 : Enemy1A {

	[Header("Enemy1A1 Specifics")]
	public Enemy babyEnemy;
	public int numberBabiesInWave;
	public float timeBetweenSpawns;
	public float timeBetweenBabies;

	protected override IEnumerator AttackRoutine(){
		timeBetweenBabies = timeBetweenBabies/2;

		yield return new WaitForSeconds (timeBetweenSpawns);

		while(hasTarget){
			sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
			
			if (Time.time > nextAttackTime && target != null && sqrDstToTarget <= Mathf.Pow(attackDistanceThreshold,2)) {
				nextAttackTime = Time.time + timeBetweenAttacks;
				yield return StartCoroutine(Attack());
			} else {
				//StopCoroutine("FollowPath"); // removed to stop weird behaviour on the path when spawning babies
				anim.SetTrigger("isSpawning");
				anim.ResetTrigger("isChasing");

				for (int i = 0; i < numberBabiesInWave; i++) {
					yield return new WaitForSeconds (timeBetweenBabies);
                    babyEnemy.isBabyEnemy = true;
					Instantiate(babyEnemy,this.transform.position,transform.rotation);
					
						// NOTE: We can't make the numberDeadEnemies change here so we have to remove this from the count.
						//enemyCountManager.sumEnemies += 1;
					yield return new WaitForSeconds (timeBetweenBabies);
				}

				//StartCoroutine("FollowPath");
				anim.ResetTrigger("isSpawning");
				anim.SetTrigger("isChasing");

				yield return new WaitForSeconds (timeBetweenSpawns); 
			}
			yield return null;
		}
	}
}
