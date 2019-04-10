using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.ThunderAndLightning;

public class Weapon : MonoBehaviour {

	protected Player player;
	protected Ship ship;

	public float msBetweenShots = 1000f;

	public float muzzleVelocity = 35f;

	[HideInInspector]
	public float nextShotTime;

    protected SoundEffectsManager soundEffectsManager;
    public AudioClip weaponFireAudio;

	protected virtual void Awake(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); 
		ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
	}

    protected virtual void Start()
    {
        soundEffectsManager = SoundEffectsManager.Instance();
    }

    public virtual void Shoot(){}
}