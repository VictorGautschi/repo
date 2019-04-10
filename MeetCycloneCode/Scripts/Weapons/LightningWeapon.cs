using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.ThunderAndLightning;
//using System.Linq;

public class LightningWeapon : Weapon {

	public GameObject muzzle;
	public float damage = 1f;
	public float energyCostPerc;
	[Tooltip("The higher this number the longer the shock carries on for, but the more expensive it is")]
	public int durationOfLightning;
	public int numberOfJumps;
    //public float stopShootingThisEnemyDistance;

	private float timeBetweenJumps = 0.05f;
	private float timeBetweenBolts = 0.05f;

	LightningBoltPrefabScript lightningBolt;

	GameObject closestGameObject;
	GameObject closestGameObject2;
	GameObject closestGameObject3;
	GameObject closestGameObject4;
	GameObject closestGameObject5;
	GameObject secondaryWeapon;

	List<GameObject> enemyList;

	[HideInInspector]
	public float shootRangeFromPlayer;

    DisplayManager displayManager;
    bool isNotAlreadyShown = true;

	protected override void Awake(){
		base.Awake();
		lightningBolt = GameObject.FindGameObjectWithTag("Lightning").GetComponent<LightningBoltPrefabScript>();
		secondaryWeapon = GameObject.FindGameObjectWithTag("Secondary Weapon");
        displayManager = DisplayManager.Instance();
	}

	public override void Shoot(){
		shootRangeFromPlayer = secondaryWeapon.gameObject.GetComponent<ShootAttack>().shootRange;

		if (ship != null){
			GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
			enemyList = new List<GameObject>(Enemies);

			if (player.energy.value >= energyCostPerc){
				if (Time.time > nextShotTime){
                    isNotAlreadyShown = true;
					nextShotTime = Time.time + msBetweenShots / 1000; //convert to milli seconds
					player.energy.value -= energyCostPerc;
                    soundEffectsManager.audioSource.PlayOneShot(weaponFireAudio, soundEffectsManager.audioSource.volume);

					if(numberOfJumps >= 1 && muzzle != null) {
						StartCoroutine(Shock());
					}

					if(numberOfJumps >= 2) {
						StartCoroutine(Shock2());
					}

					if(numberOfJumps >= 3) {
						StartCoroutine(Shock3());   
					}

					if(numberOfJumps >= 4) {
						StartCoroutine(Shock4());
					}

					if(numberOfJumps >= 5) {
						StartCoroutine(Shock5());
					}
				}
            } else {

                if (Time.time > nextShotTime && isNotAlreadyShown)
                {
                    isNotAlreadyShown = false;
                    displayManager.DisplayMessage("Recharge Energy!");
                }
            }
		}
	}

	void OnShockObject(Collider _c, Vector3 _hitPoint, float _damageModifier) { 
		IDamageable damageableObject = _c.GetComponent<IDamageable>();
		if (damageableObject != null){
            damageableObject.TakeHit((damage * _damageModifier), _hitPoint, transform.forward);
		}
	}

	public void LightningBlast(){
		StartCoroutine(ShockAll());
	}

	IEnumerator ShockAll(){
		GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
		int numLoops = 0;
		
		while(numLoops < durationOfLightning){
			if(muzzle != null) {
				for (int i = 0; i < Enemies.Length; i++) {
					if(Enemies[i] != null) {
						lightningBolt.Trigger(muzzle.transform.position,Enemies[i].transform.position);
					}
				}
			}
			yield return new WaitForSeconds (timeBetweenBolts);
			numLoops++;
		}	

		for (int i = 0; i < Enemies.Length; i++) {
			if(Enemies[i] != null) {
				OnShockObject(Enemies[i].GetComponent<Collider>(),Enemies[i].transform.position,1f);
			}
		}
		yield return null;
	}

	IEnumerator Shock() {
		int numLoops = 0;

		if (ship != null){
            closestGameObject = StaticMethods.ClosestTargetToShipWithinRange(enemyList,transform.position,ship.transform.position,shootRangeFromPlayer);

			enemyList.Remove(closestGameObject);
		}

		while(numLoops < durationOfLightning){
			if(muzzle != null && closestGameObject != null)
				lightningBolt.Trigger(muzzle.transform.position,closestGameObject.transform.position);

			yield return new WaitForSeconds (timeBetweenBolts);	
			numLoops++;
		}
		if(closestGameObject != null)
			OnShockObject(closestGameObject.GetComponent<Collider>(),closestGameObject.transform.position,1f);
		yield return null;
	}

	IEnumerator Shock2() {
		int numLoops = 0;

		if (closestGameObject != null){
			closestGameObject2 = StaticMethods.ClosestTargetToShipWithinRange(enemyList,closestGameObject.transform.position,closestGameObject.transform.position,shootRangeFromPlayer);
			enemyList.Remove(closestGameObject2);
		}

		yield return new WaitForSeconds (timeBetweenJumps);	

		while(numLoops < durationOfLightning){
			if(closestGameObject != null && closestGameObject2 != null)
				lightningBolt.Trigger(closestGameObject.transform.position,closestGameObject2.transform.position);

			yield return new WaitForSeconds (timeBetweenBolts);	
			numLoops++;
		}
		if(closestGameObject2 != null)
			OnShockObject(closestGameObject2.GetComponent<Collider>(),closestGameObject2.transform.position,0.85f);
		yield return null;
	}

	IEnumerator Shock3() {
		int numLoops = 0;

		if (closestGameObject2 != null){
			closestGameObject3 = StaticMethods.ClosestTargetToShipWithinRange(enemyList,closestGameObject2.transform.position,closestGameObject2.transform.position,shootRangeFromPlayer);
			enemyList.Remove(closestGameObject3);
		}

		yield return new WaitForSeconds (2*timeBetweenJumps);	

		while(numLoops < durationOfLightning){
			if(closestGameObject2 != null && closestGameObject3 != null)
				lightningBolt.Trigger(closestGameObject2.transform.position,closestGameObject3.transform.position);

			yield return new WaitForSeconds (timeBetweenBolts);	
			numLoops++;
		}
		if(closestGameObject3 != null)
			OnShockObject(closestGameObject3.GetComponent<Collider>(),closestGameObject3.transform.position,0.7f);
		yield return null;
	}

	IEnumerator Shock4() {
		int numLoops = 0;

		if (closestGameObject3 != null){	
			closestGameObject4 = StaticMethods.ClosestTargetToShipWithinRange(enemyList,closestGameObject3.transform.position,closestGameObject3.transform.position,shootRangeFromPlayer);
			enemyList.Remove(closestGameObject4);
		}

		yield return new WaitForSeconds (3*timeBetweenJumps);	

		while(numLoops < durationOfLightning){
			if(closestGameObject3 != null && closestGameObject4 != null)
				lightningBolt.Trigger(closestGameObject3.transform.position,closestGameObject4.transform.position);

			yield return new WaitForSeconds (timeBetweenBolts);	
			numLoops++;
		}
		if(closestGameObject4 != null)
			OnShockObject(closestGameObject4.GetComponent<Collider>(),closestGameObject4.transform.position,0.55f);
		yield return null;
	}

	IEnumerator Shock5() {
		int numLoops = 0;

		if (closestGameObject4 != null){
			closestGameObject5 = StaticMethods.ClosestTargetToShipWithinRange(enemyList,closestGameObject4.transform.position,closestGameObject4.transform.position,shootRangeFromPlayer);
			enemyList.Remove(closestGameObject5);
		}

		yield return new WaitForSeconds (4*timeBetweenJumps);	

		while(numLoops < durationOfLightning){
			if(closestGameObject4 != null && closestGameObject5 != null)
				lightningBolt.Trigger(closestGameObject4.transform.position,closestGameObject5.transform.position);

			yield return new WaitForSeconds (timeBetweenBolts);	
			numLoops++;
		}
		if(closestGameObject5 != null)
			OnShockObject(closestGameObject5.GetComponent<Collider>(),closestGameObject5.transform.position,0.4f);
		yield return null;
	}
}


/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.ThunderAndLightning;
using System.Linq;

public class LightningWeapon : Weapon {

	public GameObject muzzle;

	public float damage = 1f;
	public float energyCost;
	public int numberOfJumps;

	LightningBoltPrefabScript lightningBolt1;
	LightningBoltPrefabScript lightningBolt2;
	LightningBoltPrefabScript lightningBolt3;
	LightningBoltPrefabScript lightningBolt4;
	LightningBoltPrefabScript lightningBolt5;

	LightningBoltTransformTrackerScript lightningBoltTracker1;
	LightningBoltTransformTrackerScript lightningBoltTracker2;
	LightningBoltTransformTrackerScript lightningBoltTracker3;
	LightningBoltTransformTrackerScript lightningBoltTracker4;
	LightningBoltTransformTrackerScript lightningBoltTracker5;

	GameObject closestGameObject;
	GameObject closestGameObject2;
	GameObject closestGameObject3;
	GameObject closestGameObject4;
	GameObject closestGameObject5;
	GameObject secondaryWeapon;

	private int shootRangeFromPlayer;

	protected override void Awake(){
		base.Awake();
		lightningBolt1 = GameObject.FindGameObjectWithTag("Lightning1").GetComponent<LightningBoltPrefabScript>();
		lightningBolt2 = GameObject.FindGameObjectWithTag("Lightning2").GetComponent<LightningBoltPrefabScript>();
		lightningBolt3 = GameObject.FindGameObjectWithTag("Lightning3").GetComponent<LightningBoltPrefabScript>();
		lightningBolt4 = GameObject.FindGameObjectWithTag("Lightning4").GetComponent<LightningBoltPrefabScript>();
		lightningBolt5 = GameObject.FindGameObjectWithTag("Lightning5").GetComponent<LightningBoltPrefabScript>();

		lightningBoltTracker1 = GameObject.FindGameObjectWithTag("Lightning1").GetComponent<LightningBoltTransformTrackerScript>();
		lightningBoltTracker2 = GameObject.FindGameObjectWithTag("Lightning2").GetComponent<LightningBoltTransformTrackerScript>();
		lightningBoltTracker3 = GameObject.FindGameObjectWithTag("Lightning3").GetComponent<LightningBoltTransformTrackerScript>();
		lightningBoltTracker4 = GameObject.FindGameObjectWithTag("Lightning4").GetComponent<LightningBoltTransformTrackerScript>();
		lightningBoltTracker5 = GameObject.FindGameObjectWithTag("Lightning5").GetComponent<LightningBoltTransformTrackerScript>();

		secondaryWeapon = GameObject.FindGameObjectWithTag("Secondary Weapon");
		shootRangeFromPlayer = secondaryWeapon.gameObject.GetComponent<ShootAttack>().shootRange;
	}

	public override void Shoot(){

		if (ship != null){
			GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
			List<GameObject> enemyList = new List<GameObject>(Enemies);

			if (ship != null){
				closestGameObject = StaticMethods.ClosestTargetToShipWithinRange(enemyList,transform.position,ship.transform.position,shootRangeFromPlayer);
				enemyList.Remove(closestGameObject);
			}
			if (closestGameObject != null){
				closestGameObject2 = StaticMethods.ClosestTargetToShipWithinRange(enemyList,closestGameObject.transform.position,closestGameObject.transform.position,shootRangeFromPlayer);
				enemyList.Remove(closestGameObject2);
			}
			if (closestGameObject2 != null){
				closestGameObject3 = StaticMethods.ClosestTargetToShipWithinRange(enemyList,closestGameObject2.transform.position,closestGameObject2.transform.position,shootRangeFromPlayer);
				enemyList.Remove(closestGameObject3);
			}
			if (closestGameObject3 != null){	
				closestGameObject4 = StaticMethods.ClosestTargetToShipWithinRange(enemyList,closestGameObject3.transform.position,closestGameObject3.transform.position,shootRangeFromPlayer);
				enemyList.Remove(closestGameObject4);
			}
			if (closestGameObject4 != null){
				closestGameObject5 = StaticMethods.ClosestTargetToShipWithinRange(enemyList,closestGameObject4.transform.position,closestGameObject4.transform.position,shootRangeFromPlayer);
				enemyList.Remove(closestGameObject5);
			}

			if (player.energy.CurrentVal >= energyCost){
				if (Time.time > nextShotTime){
					nextShotTime = Time.time + msBetweenShots / 1000; //convert to milli seconds
					player.energy.CurrentVal -= energyCost;

					if(numberOfJumps >= 1 && closestGameObject != null) {
						lightningBolt1.Trigger(muzzle.transform.position,closestGameObject.transform.position);		
						OnShockObject(closestGameObject.GetComponent<Collider>(),closestGameObject.transform.position);
						lightningBoltTracker1.StartTarget = muzzle.transform;
						lightningBoltTracker1.EndTarget = closestGameObject.transform;
					}

					if(numberOfJumps >= 2 && closestGameObject2 != null) {
						lightningBolt2.Trigger(closestGameObject.transform.position,closestGameObject2.transform.position);
						OnShockObject(closestGameObject2.GetComponent<Collider>(),closestGameObject2.transform.position);
						lightningBoltTracker2.StartTarget = closestGameObject.transform;
						lightningBoltTracker2.EndTarget = closestGameObject2.transform;
					}

					if(numberOfJumps >= 3 && closestGameObject3 != null) {
						lightningBolt3.Trigger(closestGameObject2.transform.position,closestGameObject3.transform.position);
						OnShockObject(closestGameObject3.GetComponent<Collider>(),closestGameObject3.transform.position);
						lightningBoltTracker3.StartTarget = closestGameObject2.transform;
						lightningBoltTracker3.EndTarget = closestGameObject3.transform;
					}

					if(numberOfJumps >= 4 && closestGameObject4 != null) {
						lightningBolt4.Trigger(closestGameObject3.transform.position,closestGameObject4.transform.position);
						OnShockObject(closestGameObject4.GetComponent<Collider>(),closestGameObject4.transform.position);
						lightningBoltTracker4.StartTarget = closestGameObject3.transform;
						lightningBoltTracker4.EndTarget = closestGameObject4.transform;
					}

					if(numberOfJumps >= 5 && closestGameObject5 != null) {
						lightningBolt5.Trigger(closestGameObject4.transform.position,closestGameObject5.transform.position);
						OnShockObject(closestGameObject5.GetComponent<Collider>(),closestGameObject5.transform.position);
						lightningBoltTracker5.StartTarget = closestGameObject4.transform;
						lightningBoltTracker5.EndTarget = closestGameObject5.transform;
					}
				}
			}
		}
	}

	void OnShockObject(Collider _c, Vector3 _hitPoint) { 
		IDamageable damageableObject = _c.GetComponent<IDamageable>();
		if (damageableObject != null){
			damageableObject.TakeHit(damage, _hitPoint, transform.forward);
		}
	}
} */


