using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour {

	Transform target;
	Camera gameCamera;

    [HideInInspector]
    public bool followingPath;

	public bool displayUnitGizmos;
	public float speed;
	//float turnSpeed = 10000f; // for future projects
	float turnDst = 0f; // for future projects

	[Header("Walkable Regions")]
	public bool ignoreUnwalkable = false; // future development for flying creatures

	const float minPathUpdateTime = 2f; // this time seems to affect the jump forward - 0.2f and it jumps, but above 1f and it doesn't
	const float pathUpdateMoveThreshold = .5f;

	Path path;

	void Start () {
		gameCamera = Camera.main;
	}	

	void Update (){
		if (Input.GetMouseButtonUp(0)){
			Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitinfo;

			if (Physics.Raycast (ray, out hitinfo)) {
				if (!StaticMethods.IsPointerOverUIObject()){
					target = hitinfo.collider.transform;
                    StopCoroutine("UpdatePath");
                    StartCoroutine("UpdatePath");
					// StartCoroutine(UpdatePath());	
				}
			}
		}
	}

	public void OnPathFound (Vector3[] _waypoints, bool _pathSuccessful) {
		if (_pathSuccessful && (gameObject != null)) {
			path = new Path(_waypoints, transform.position, turnDst);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath() {
		if(Time.timeSinceLevelLoad < .5f){
			yield return new WaitForSeconds (.5f);
		}
		PathRequestManager.RequestPath(new PathRequest(transform.position,target.position,OnPathFound), ignoreUnwalkable);

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true) {
			yield return new WaitForSeconds (minPathUpdateTime);
			if(target != null) {
				if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
					PathRequestManager.RequestPath(new PathRequest(transform.position,target.position,OnPathFound), ignoreUnwalkable);
					targetPosOld = target.position;
				}
			}
		}
	}

	IEnumerator FollowPath() {

		followingPath = true;
		int pathIndex = 0;

		if(path != null && path.lookPoints.Length > 0) {

			transform.LookAt(path.lookPoints[0]); // maybe change to look at closest enemy and not the next waypoint??
			float speedPercent = 1;

			while (followingPath) {
				Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
				while (path.turnBoundaries[pathIndex].HasCrossedLine (pos2D)) {
					if (pathIndex == path.finishLineIndex){
						followingPath = false;
						break;
					} else {
						pathIndex++;
					}
				}

				// rotates unit to look forward when moving, and moves it in the direction it is looking
			 	/*if (followingPath) {
					// Cyclone must look in the direction of the closest enemy, else if no enemy then in the direction he is going. If not moving and no enemy then at the ship.
					// Currently the CycloneLook script is doing that
					Vector3 relativePos = path.lookPoints [pathIndex] - transform.position;
					Quaternion targetRotation = Quaternion.LookRotation(relativePos);
					transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
					transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);

				} */

				// moves in the direction of the next waypoint without affecting the rotation
				if (followingPath) {
					Vector3 direction = path.lookPoints[pathIndex] - transform.position;
					transform.rotation = Quaternion.identity; // Rotation is dealt with in PlayerLook and WeaponLook - identity = no rotation.
					transform.Translate(Vector3.Normalize(direction) * Time.deltaTime * speed * speedPercent, Space.Self);
				} 
				yield return null;
			}
		}
		yield return null;
	}

	public void OnDrawGizmos() {
		if (path != null && displayUnitGizmos) {
			path.DrawWithGizmos();
		}
	}
}