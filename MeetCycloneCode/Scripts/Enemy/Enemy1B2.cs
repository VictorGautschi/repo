using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This enemy goes for the player if he has energy and then for the ship if not. Can only attack once.

public class Enemy1B2 : Enemy1B {

	[Header("Enemy1B2 Specifics")]
	[Tooltip("the speed at which this enemy leaps during its attack")]
	public float attackSpeed = 2.5f;

	protected EnemyCountManager enemyCountManager;

	protected override void Awake(){
		base.Awake();
		enemyCountManager = EnemyCountManager.Instance();
        //Debug.Log(isBabyEnemy);
	}

	protected override IEnumerator AttackRoutine(){
		while (hasTarget) { 
			sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

			if (Time.time > nextAttackTime && target != null && sqrDstToTarget <= Mathf.Pow(attackDistanceThreshold,2)) {
				nextAttackTime = Time.time + timeBetweenAttacks;
				yield return StartCoroutine(Attack());
			}
			yield return null;
		} 
	}

	protected override IEnumerator Attack() {

		StopCoroutine("FollowPath"); // stop the pathfinding

		Vector3 originalPosition = transform.position;
		Vector3 dirToTarget = (target.position - transform.position).normalized; 
		Vector3 attackPosition = target.position - dirToTarget * (0.1f); // The bracket changes the attackPosition to less so it intersects the target

		float percent = 0;

		bool hasAppliedDamage = false;
				
		while (percent <= 1) {

			if(percent >= 0.5f && !hasAppliedDamage) { // 0.5 percent is when the enemy is halfway through its lunge, which is when it hits the player. 
				hasAppliedDamage = true;

				if (target != null) {
					if(target == player.transform){
						player.EnergyDrain(energyDrainPerc);

                        /*  If this is a baby enemy then don't add to the dead enemy count as the total enemies is 
                            calculated in the EnemySpawner and so the baby enemy is not counted. So we can't see 
                            when sumEnemies == noDeadEnemies. isBabyEnemy is set in the Script of the Enemy that spawns it */
                        if (!isBabyEnemy)
                        {
                            enemyCountManager.AddDeadEnemies();
                        }
                        
						Destroy(gameObject);
						//Destroy(Instantiate(energyDrainEffect.gameObject,target.transform.position, Quaternion.FromToRotation(Vector3.forward, dirToTarget)) as GameObject, deathEffect.main.startLifetimeMultiplier); 
					} else {
						ship.TakeDamage(damagePerc);

                        /*  If this is a baby enemy then don't add to the dead enemy count as the total enemies is 
                            calculated in the EnemySpawner and so the baby enemy is not counted. So we can't see 
                            when sumEnemies == noDeadEnemies. isBabyEnemy is set in the Script of the Enemy that spawns it */
                        if (!isBabyEnemy)
                        {
                            enemyCountManager.AddDeadEnemies();
                        }

                        Destroy(Instantiate(hitShipEffect.gameObject,this.transform.position, Quaternion.FromToRotation(Vector3.forward, dirToTarget)) as GameObject, hitShipEffect.main.startLifetimeMultiplier); 
						Destroy(gameObject);
					}

					

					
				}
			}

			percent += Time.deltaTime * attackSpeed;
			transform.position = Vector3.Lerp(originalPosition, attackPosition, percent); 

			anim.SetTrigger("isAttacking");
			anim.ResetTrigger("isChasing");

			yield return null;	
		} 
	}
}
