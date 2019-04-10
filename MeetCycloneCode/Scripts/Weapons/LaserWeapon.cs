using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.ThunderAndLightning;


public class LaserWeapon : Weapon {

	public GameObject muzzle;
	public float damage = 1f;
	[Tooltip("The higher this number the longer the beam carries on for, but the more expensive it is")]
	public int durationOfBeam;

	private float timeBetweenBeams = 0.05f;

	LightningBoltPrefabScript laserBeam;

	GameObject closestGameObject;

	List<GameObject> enemyList;

	[HideInInspector]
	public float shootRangeFromThisWeapon;

	protected override void Awake(){
		base.Awake();
		laserBeam = GameObject.FindGameObjectWithTag("Laser Beam").GetComponent<LightningBoltPrefabScript>();
	}

	public override void Shoot(){
		shootRangeFromThisWeapon = transform.GetComponentInParent<ShootAttack>().shootRange;

		if (ship != null){
			GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
			enemyList = new List<GameObject>(Enemies);

			if (Time.time > nextShotTime){
				nextShotTime = Time.time + msBetweenShots / 1000; //convert to milli seconds

				if (muzzle != null) {
					StartCoroutine(Beam());
                    soundEffectsManager.audioSource.PlayOneShot(weaponFireAudio, soundEffectsManager.audioSource.volume);
				}
			}
		}
	}

	void OnLaserObject(Collider _c, Vector3 _hitPoint) { 
		IDamageable damageableObject = _c.GetComponent<IDamageable>();
		if (damageableObject != null){
			damageableObject.TakeHit(damage, _hitPoint, transform.forward);
		}
	}

	IEnumerator Beam() {
		int numLoops = 0;

		if (ship != null){
			closestGameObject = StaticMethods.ClosestTargetToShipWithinRange(enemyList,transform.position,transform.position,shootRangeFromThisWeapon);
			enemyList.Remove(closestGameObject);
		}

		while(numLoops < durationOfBeam){
			if(muzzle != null && closestGameObject != null)
				laserBeam.Trigger(muzzle.transform.position,closestGameObject.transform.position);

			yield return new WaitForSeconds (timeBetweenBeams);	
			numLoops++;
		}
		if(closestGameObject != null)
			OnLaserObject(closestGameObject.GetComponent<Collider>(),closestGameObject.transform.position);
		yield return null;
	}
}
