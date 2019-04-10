using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy {

	protected override IEnumerator Attack() {

		currentState = State.Attacking;
		StopCoroutine("FollowPath"); // stop the pathfinding

		Vector3 originalPosition = transform.position;
		Vector3 dirToTarget = (target.position - transform.position).normalized;
		Vector3 attackPosition = target.position - dirToTarget * (1);

		float attackSpeed = 3;
		float percent = 0;

		skinMaterial.color = Color.blue;
		bool hasAppliedDamage = false;

		while (percent <= 1) {

			if(percent >= 0.5f && !hasAppliedDamage) { // 0.5 percent is when the enemy is halfway through its lunge, which is when it hits the player. At this point it causes damage
				hasAppliedDamage = true;
				targetEntity.TakeDamage(damagePerc);
			}

			percent += Time.deltaTime * attackSpeed;
			float interpolation = (-Mathf.Pow(percent,2) + percent) * 4; // porabola equation
			transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation); // at interpolation 0 then originalPosition, at interpolation 1 then attack position, and back again

			yield return null;	
		}
		skinMaterial.color = originalColor;
		currentState = State.Chasing;
	}
}
