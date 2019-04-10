using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable {

	[Header("Testing")]
	public bool displayUnitGizmos;

	[Header("Statistics")]
	public float startingHealth;
	protected float currentHealth; // not public but the derived classes such as Player and Enemy can still access it
	protected bool dead;

	// an event is always of type delegate
	public event System.Action OnDeath; // creates an event called OnDeath

	protected virtual void Start() { 
		currentHealth = startingHealth;
	}

	public virtual void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDirection) {
		// Do some stuff here with _hit variable
		TakeDamage(_damage); 
	}

	public virtual void TakeDamage(float _damage) { 
		currentHealth -= _damage; 

		if (currentHealth <= 0 && !dead) { // not already dead
			Die();
		}
	}

	protected void Die(){ 
		dead = true;
		if(OnDeath != null) { // handles the event
			OnDeath(); // This publishes the event when the GameObject is destroyed and all the subscribing methods will get that information (see EnemySpawner.cs which has the subscriber)
		}
		Object.Destroy(gameObject);
	}

	protected void DontDestroy(){
		dead = true;
		if(OnDeath != null) { // handles the event
			OnDeath(); // This publishes the event when the GameObject is destroyed and all the subscribing methods will get that information (see EnemySpawner.cs which has the subscriber)
		}
	}
}
