using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This enemy attacks the ship only.

public class Enemy1A : Enemy {

	[Header("Enemy1A Specifics")]
	[Tooltip("Attack speed when leaping into ship. Not relevant to ranged units A")]
	public float attackSpeed = 2.5f;
	[Tooltip("Collision Effect when leaping into ship. Not relevant to ranged units A")]
	public ParticleSystem collisionEffect;

	protected EnemyCountManager enemyCountManager;

	protected override void Awake(){
		base.Awake();
		anim = GetComponentInChildren<Animator>();
		enemyCountManager = EnemyCountManager.Instance();
	}

	protected override void Start () {
		currentHealth = startingHealth;
		skinMaterial = GetComponentInChildren<Renderer>().material;
		originalColor = skinMaterial.color;

		if (ship != null) { 
			hasTarget = true;

			target = ship.transform; 
			targetEntity = target.transform.GetComponent<LivingEntity>(); 
			targetEntity.OnDeath += OnTargetDeath; 

			StartCoroutine(UpdatePath());
			StartCoroutine(AttackRoutine());

			if(isBoss)
				StartCoroutine(RegenerateHealthOverTime());
		}		
	}

	protected virtual IEnumerator AttackRoutine(){
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

                if (target != null)
                {
                    ship.TakeDamage(damagePerc);
                    //Destroy(Instantiate(collisionEffect.gameObject,this.transform.position, Quaternion.FromToRotation(Vector3.forward, dirToTarget)) as GameObject, deathEffect.main.startLifetimeMultiplier); // changed from deathEffect.startLifetime
                    //Instantiate(collisionEffect.gameObject, this.transform.position, Quaternion.FromToRotation(Vector3.forward, dirToTarget)); // changed from deathEffect.startLifetime

                    // The following is hardcoded for the timw being, must be changed
                    Instantiate(collisionEffect.gameObject, new Vector3(this.transform.position.x, this.transform.position.y - 0.2f, this.transform.position.z), Quaternion.Euler(-90f, 0f, 0f)); // using this because the particle effect is not the right way up

                    /* If this is a baby enemy then don't add to the dead enemy count as the total enemies is 
					   calculated in the EnemySpawner and so the baby enemy is not counted. So we can't see 
					   when sumEnemies == noDeadEnemies. isBabyEnemy is set in the Script of the Enemy that spawns it */
                    if (!isBabyEnemy)
                    {
                        enemyCountManager.AddDeadEnemies();
                        //Debug.Log("Enemy Dies in Enemy1A");
                    }

                    Destroy(gameObject);
                }
			}

			percent += Time.deltaTime * attackSpeed;
			transform.position = Vector3.Lerp(originalPosition, attackPosition, percent); 
			//anim.ResetTrigger("isChasing");
			anim.SetTrigger("isAttacking");
			yield return null;	
		} 
	}
}
