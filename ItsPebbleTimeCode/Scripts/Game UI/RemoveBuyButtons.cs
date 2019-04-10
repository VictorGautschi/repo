using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBuyButtons : MonoBehaviour {

    public GameObject unlockDoodleButton;
    public GameObject removeAdsButton;
    public GameObject storeButton;

	void Awake () {
        GameControl.control.Load();

        if(!GameControl.control.adsDisabled)
        {
            unlockDoodleButton.gameObject.SetActive(true);
#if UNITY_IOS
            removeAdsButton.gameObject.SetActive(true);
#endif
            storeButton.gameObject.SetActive(true);
        }
	}
}
