using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour {

	void OnEnable() { 					// when this object is enabled in inspector,
		transform.SetAsLastSibling();   // make this the last child in the heirarchy for its layer and it will draw last (on top)
    }
}
