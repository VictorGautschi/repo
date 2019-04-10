using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour {

	public float perspectiveZoomSpeed = 0.5f;
	public float orthoZoomSpeed = 0.5f;
	public float pinchToZoomMin = 0.1f;
	public float pinchToZoomMax = 179.9f;

	void update () {
		if (Input.touchCount == 2) {
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if(Camera.main.orthographic){
				Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f); //stop it from inverting
			} else {
				Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, pinchToZoomMin, pinchToZoomMax);
			}
		}
	}
}
