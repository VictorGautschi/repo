using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWeapon : Weapon {

	public Transform muzzle;

	[SerializeField]
	protected Projectile projectile;

	public override void Shoot(){
		if (Time.time > nextShotTime){
			nextShotTime = Time.time + msBetweenShots / 1000; //convert to milli seconds
			Projectile newProjectile = Instantiate(projectile,muzzle.position,muzzle.rotation) as Projectile;
			newProjectile.SetSpeed(muzzleVelocity);
            soundEffectsManager.audioSource.PlayOneShot(weaponFireAudio, soundEffectsManager.audioSource.volume);
		}
	}
}
