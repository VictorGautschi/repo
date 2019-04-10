using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// This is an enemy projectile used against the ship and/or player

public class SpellProjectile : Projectile {

	protected override void Start () {
		base.Start();

		if(target == Target.ship){
			closestGameObject = ship.gameObject;
		} else {
			closestGameObject = player.gameObject;
		}
	}
}