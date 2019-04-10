using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon {

	public Transform muzzle;

	[SerializeField]
	protected Projectile projectile;

    List<Projectile> projectileList;

    protected override void Start()
    {
        base.Start();

        projectileList = new List<Projectile>();
        for (int i = 0; i < 10; i++)
        {
            Projectile newProjectile = (Projectile)Instantiate(projectile);
            newProjectile.gameObject.SetActive(false);
            projectileList.Add(newProjectile);
        }
    }

    public override void Shoot(){
        
        if (Time.time > nextShotTime)
        { 
            nextShotTime = Time.time + msBetweenShots / 1000; //convert to milli seconds 
            for (int i = 0; i < projectileList.Count; i++)
            {
         
            if (!projectileList[i].gameObject.activeInHierarchy)
            {
                    projectileList[i].gameObject.transform.position = muzzle.position;
                    projectileList[i].gameObject.transform.rotation = muzzle.rotation;
                    projectileList[i].SetSpeed(muzzleVelocity);
                    projectileList[i].gameObject.SetActive(true);
                    break;
                }
            }
            soundEffectsManager.audioSource.PlayOneShot(weaponFireAudio, soundEffectsManager.audioSource.volume);
       }
   


        // This will sty as a reminder. If we ever want to introduce missiles or other energy using projectiles.

        /* if (projectile.energyCost <= 0f){
			if (Time.time > nextShotTime){
				nextShotTime = Time.time + msBetweenShots / 1000; //convert to milli seconds
				Projectile newProjectile = Instantiate(projectile,muzzle.position,muzzle.rotation) as Projectile;
				newProjectile.SetSpeed(muzzleVelocity);
                soundEffectsManager.audioSource.PlayOneShot(weaponFireAudio, soundEffectsManager.audioSource.volume);
			}
		} else {
			if (player.energy.value >= projectile.energyCost/100){
				if (Time.time > nextShotTime){
					nextShotTime = Time.time + msBetweenShots / 1000; //convert to milli seconds
					Projectile newProjectile = Instantiate(projectile,muzzle.position,muzzle.rotation) as Projectile;
					newProjectile.SetSpeed(muzzleVelocity);
                    soundEffectsManager.audioSource.PlayOneShot(weaponFireAudio, soundEffectsManager.audioSource.volume);
					player.energy.value -= projectile.energyCost/100;
				}
			}
		}*/	
	}
}