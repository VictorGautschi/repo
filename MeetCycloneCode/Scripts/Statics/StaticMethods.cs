using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticMethods : MonoBehaviour {

	public static GameObject ClosestTargetToShipWithinRange(List<GameObject> targetsToConsider, Vector3 thisPosition, Vector3 anchorPosition, float shootRange){
		float smallestDistance = Mathf.Infinity;
		GameObject closestTarget = null;

		foreach(var go in targetsToConsider) {
            
			Vector3 directionToTarget = go.transform.position - anchorPosition;
			var distance = Vector3.Distance(thisPosition,go.transform.position);

            if (directionToTarget.sqrMagnitude < smallestDistance && distance <= shootRange && directionToTarget.sqrMagnitude > (1.1 * 1.1)) {
				smallestDistance = directionToTarget.sqrMagnitude;
				closestTarget = go;
	    	}  
	  	}
		return closestTarget;
	}

    public static GameObject ClosestTargetToPlayerWithinRange(List<GameObject> targetsToConsider, Vector3 anchorPosition)
    {
        float smallestDistance = Mathf.Infinity;
        GameObject closestTarget = null;

        foreach (var go in targetsToConsider)
        {
            Vector3 directionToTarget = go.transform.position - anchorPosition;

            if (directionToTarget.sqrMagnitude < smallestDistance)
            {
                smallestDistance = directionToTarget.sqrMagnitude;
                closestTarget = go;
            }
        }
        return closestTarget;
    }

	public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
