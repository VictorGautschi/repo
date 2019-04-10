using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (WeaponController))]
public class ShootAttack : MonoBehaviour {

	WeaponController weaponController;
	Ship ship;
    Player player;
	GameObject closestGameObject;

	public enum Target{enemyClosestToPlayer,enemyClosestToShip,shipsTarget};
	public Target target; 

	public bool weaponDisabled = false;

	public float shootRange = 10;
	public float turnSpeed = 10f;

	List<GameObject> Enemies = new List<GameObject>();
	Vector3 lastPosition = new Vector3();
	Vector3 currentPosition = new Vector3();

	void Start () {
		weaponController = GetComponent<WeaponController>();
		ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	protected virtual void Update () {

		currentPosition = GetComponent<Transform>().position;
		Enemies.Clear(); // To stop null pointer exception when an enemy dies. Expensive???

        if (target == Target.enemyClosestToPlayer)
        {
            Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            closestGameObject = StaticMethods.ClosestTargetToPlayerWithinRange(Enemies,player.transform.position);
        }

		if (target == Target.enemyClosestToShip && ship != null){
			Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
			closestGameObject = StaticMethods.ClosestTargetToShipWithinRange(Enemies,transform.position,ship.transform.position,shootRange);
		} 

        if (target == Target.shipsTarget && ship != null){
            Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            closestGameObject = StaticMethods.ClosestTargetToShipWithinRange(Enemies, transform.position, transform.position, shootRange);
        }

		if (closestGameObject != null) {
			float dist = Vector3.Distance(closestGameObject.transform.position,transform.position);
			if(dist <= shootRange && !weaponDisabled){
				Vector3 relativePos = closestGameObject.transform.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativePos);
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
				weaponController.Shoot();
			} else {
				Vector3 relativePos = closestGameObject.transform.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativePos);
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
			}
		} else {
			if (currentPosition == lastPosition){ // if the object that this is on is not moving
				if(target == Target.enemyClosestToShip){
					transform.rotation = GameObject.FindGameObjectWithTag("Player Rotation").GetComponent<Transform>().rotation;
				} else {
					if (ship != null && target != Target.shipsTarget) {
						transform.rotation = GameObject.FindGameObjectWithTag("Player Rotation").GetComponent<Transform>().rotation;
					} else {
                        Vector3 relativePos = new Vector3(Camera.main.transform.position.x, 0f, Camera.main.transform.position.z) - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(relativePos);
                        transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
					}
				}
			} else {
				if (target != Target.shipsTarget){
					transform.rotation = GameObject.FindGameObjectWithTag("Player Rotation").GetComponent<Transform>().rotation;
				} else {
                    Vector3 relativePos = new Vector3(Camera.main.transform.position.x, 0f, Camera.main.transform.position.z) - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(relativePos);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
                }
            }		
			lastPosition = currentPosition;	
		}
	}
}