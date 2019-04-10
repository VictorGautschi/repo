using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Missile : Projectile {
    
	GameObject secondaryWeapon;

	List<GameObject> Enemies = new List<GameObject>();

	float shootRange;

	protected override void Start() { 
		base.Start();
		secondaryWeapon = GameObject.FindGameObjectWithTag("Secondary Weapon");
		shootRange = secondaryWeapon.gameObject.GetComponent<ShootAttack>().shootRange;

		if (ship != null){
			Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
			closestGameObject = StaticMethods.ClosestTargetToShipWithinRange(Enemies,transform.position,ship.transform.position,shootRange);
		}
	}
}




//// old one incase and for reference
//protected override void LaunchProjectile(){
//
//	float moveDistance = speed * Time.deltaTime;
//	CheckCollisions (moveDistance);
//				
//	if (homing) { // This will need to change accordingly when ShootAttack changes to attack the closest to ship within range. Call ShootAttack and use its target.
//		closestGameObject = GameObject.FindGameObjectsWithTag("Enemy").OrderBy(go => Vector3.Distance(go.transform.position,ship.transform.position)).FirstOrDefault();
//		if(closestGameObject != null){
//			float dist = Vector3.Distance(closestGameObject.transform.position, transform.position);
//
//			if (dist < homingRange) {
//				
//				homingMissile.velocity = transform.forward * speed;
//				Quaternion rotation = Quaternion.LookRotation(closestGameObject.transform.position - transform.position);	
//				homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, 45));	
//
//			} else {
//				transform.Translate(Vector3.forward * moveDistance);
//			}
//		} else {
//			GameObject.Destroy(gameObject);
//			// Destroying effect of missile
//		}
//	} else {
//		transform.Translate(Vector3.forward * moveDistance); 
//	}
//}

