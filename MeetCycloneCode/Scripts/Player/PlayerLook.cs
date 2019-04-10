using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class PlayerLook : MonoBehaviour {

	float turnSpeed = 5f;

    PlayerMovement playerMovement;

	//Ship ship;

    Animator anim;

	void Start() {
		//ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }
	
	void Update () {
		var closestGameObject = GameObject.FindGameObjectsWithTag("Enemy").OrderBy(go => Vector3.Distance(go.transform.position,transform.position)).FirstOrDefault();

		if (closestGameObject != null) {
            anim.SetTrigger("isNormal");
            anim.ResetTrigger("isScared");
            Vector3 relativePos = closestGameObject.transform.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
		} else {
            if(playerMovement.followingPath){
                anim.SetTrigger("isNormal");
                anim.ResetTrigger("isScared");
            } else {
                anim.SetTrigger("isScared");
                anim.ResetTrigger("isNormal");
            }

            Vector3 relativePos = new Vector3(Camera.main.transform.position.x,0f,Camera.main.transform.position.z) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

            /* if (ship != null) {
                 anim.SetTrigger("isScared");
                anim.ResetTrigger("isNormal");
                Vector3 relativePos = ship.transform.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativePos);
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * turnSpeed); 
			} */
		}
	}
}
