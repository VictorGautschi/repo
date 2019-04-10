using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public Transform weaponHold;
	public Weapon startingWeapon;

	[HideInInspector]
	public Weapon equippedWeapon;

    Player player;

    void Awake()
    {
        if (weaponHold == null)
        {
            weaponHold = this.transform;
        }
        if (startingWeapon != null)
        {
            EquipWeapon(startingWeapon);
        }
    }

	void Start (){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	public void EquipWeapon(Weapon gunToEquip) {
		if (equippedWeapon != null) {
			Destroy(equippedWeapon.gameObject);
		}
		equippedWeapon = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Weapon; // instantiate the gun at the players weaponHold
		equippedWeapon.transform.parent = weaponHold; // the weaponHold becomes the equipped guns parent
	}

	public void Shoot(){
        if (equippedWeapon != null && player.weaponActive && (tag == "Secondary Weapon" || tag == "Primary Weapon")) {
			equippedWeapon.Shoot();
		}

        if(equippedWeapon != null && tag != "Secondary Weapon" && tag != "Primary Weapon"){
            equippedWeapon.Shoot();
        }

	}
}