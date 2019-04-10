using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSetActiveFalse : MonoBehaviour {
    
	public GameObject toSetActiveFalse;

    public void SetActiveFalse()
    {
        toSetActiveFalse.gameObject.SetActive(false);
    }
}
