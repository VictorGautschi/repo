using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This enemy is ranged and attacks both ship and player.

public class Enemy1B3 : Enemy1B {

	[Header("Enemy1B3 Specifics")]
	[Tooltip("The weapons are changed as the targets change, this is the weapon used to attack the Ship")]
	public Weapon weaponToAttackShip;
	[Tooltip("The weapons are changed as the targets change, this is the weapon used to attack the Player")]
	public Weapon weaponToAttackPlayer;
	[Tooltip("The spell that shows up before it is thrown at the Player. The actual projectile is added to the wepaon. This is for show")]
	public GameObject fakeProjectilePlayer;
	[Tooltip("The spell that shows up before it is thrown at the Ship. The actual projectile is added to the wepaon. This is for show")]
	public GameObject fakeProjectileShip;
	[Tooltip("The point where the fake projectiles will be spawned")]
	public Transform projectileSpawnPoint;
	[Tooltip("When the enemy gets this close to the ship, it will start throwing spells")]
	public float startShootDistanceToShip;
	[Tooltip("When cyclone gets this close to the enemy, the enemy will start to shoot")]
	public float startShootDistanceToCyclone;
	[Tooltip("When cyclone gets this close to the enemy, the enemy will not shoot")]
	public float stopShootDistanceToCyclone;
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

		yield return new WaitForSeconds (timeBetweenAttacks); // Length of the animation

		while(hasTarget){

			sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

			if(target == player.transform){

				if(newProjectile != fakeProjectilePlayer && newProjectile != null)
					Destroy(newProjectile);

				weaponController.EquipWeapon(weaponToAttackPlayer);
				
				if(sqrDstToTarget <= Mathf.Pow(startShootDistanceToCyclone,2)){
					if(sqrDstToTarget <= Mathf.Pow(stopShootDistanceToCyclone,2)){
						anim.SetTrigger("isChasing");
						anim.ResetTrigger("isAttacking");
						anim.ResetTrigger("isThrowing");

						if(sqrDstToTarget <= Mathf.Pow(attackDistanceThreshold,2)){
							// Must stop pathfollowing here so that enemy does not keep following the path until nextAttackTime
							StopCoroutine("FollowPath"); 
							if (Time.time >= nextAttackTime) {
								nextAttackTime = Time.time + timeBetweenAttacks;
								yield return StartCoroutine(Attack());
							}
						}
					} else {
						if (Time.time > nextSpellTime) {
							nextSpellTime = Time.time + timeBetweenSpells;

							//StopCoroutine("FollowPath");
							anim.SetTrigger("isThrowing");
							anim.ResetTrigger("isChasing");
							anim.ResetTrigger("isAttacking");

							// When one is not available then the other is
							if(newProjectile == null){
								newProjectile = Instantiate(fakeProjectilePlayer,projectileSpawnPoint.transform.position,Quaternion.identity) as GameObject;
								newProjectile.transform.SetParent(projectileSpawnPoint.transform);
							}					

							yield return new WaitForSeconds (timeBetweenAttacks/4);
							Destroy(newProjectile);
							weaponController.Shoot();

							yield return new WaitForSeconds(timeBetweenAttacks/4);

							// When one is not available then the other is
							if(newProjectile == null){
								newProjectile = Instantiate(fakeProjectilePlayer,projectileSpawnPoint.transform.position,Quaternion.identity) as GameObject;
								newProjectile.transform.SetParent(projectileSpawnPoint.transform);	
							}

							/* NOTE: As long as the total wait time in here is less than the timeBetweenAttacks, 
							   the spell will be cast at the right time. */

						}
					}
				} else {
					anim.SetTrigger("isChasing");
					anim.ResetTrigger("isAttacking");
					anim.ResetTrigger("isThrowing");
				}
			} else {

				if(newProjectile != fakeProjectileShip && newProjectile != null)
					Destroy(newProjectile);
				
				weaponController.EquipWeapon(weaponToAttackShip);

				anim.SetTrigger("isChasing");
				anim.ResetTrigger("isAttacking");
				anim.ResetTrigger("isThrowing");

				if (sqrDstToTarget <= Mathf.Pow(startShootDistanceToShip,2)){

					if(sqrDstToTarget > Mathf.Pow(attackDistanceThreshold,2)){

						if (Time.time > nextSpellTime && target != null) {
							nextSpellTime = Time.time + timeBetweenSpells;

							StopCoroutine("FollowPath");
							anim.SetTrigger("isThrowing");
							anim.ResetTrigger("isChasing");
							anim.ResetTrigger("isAttacking");

							// When one is not available then the other is
							if(newProjectile == null){
								newProjectile = Instantiate(fakeProjectileShip,projectileSpawnPoint.transform.position,Quaternion.identity) as GameObject;
								newProjectile.transform.SetParent(projectileSpawnPoint.transform);
							}					

							yield return new WaitForSeconds (timeBetweenAttacks/4);
							Destroy(newProjectile);
							weaponController.Shoot();

							yield return new WaitForSeconds(timeBetweenAttacks/4);

							// When one is not available then the other is
							if(newProjectile == null){
								newProjectile = Instantiate(fakeProjectileShip,projectileSpawnPoint.transform.position,Quaternion.identity) as GameObject;
								newProjectile.transform.SetParent(projectileSpawnPoint.transform);	
							}

							/* NOTE: As long as the total wait time in here is less than the timeBetweenAttacks, 
							   the spell will be cast at the right time. */

						}
					} else {
						// Must stop pathfollowing here so that enemy does not keep following the path until nextAttackTime
						StopCoroutine("FollowPath"); 
						if (Time.time >= nextAttackTime) {
							nextAttackTime = Time.time + timeBetweenAttacks;
							yield return StartCoroutine(Attack());
						}
					}
				} 
			}
			yield return null;
		}
	}
}
