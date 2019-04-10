using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveShopButton : MonoBehaviour {

    public GameObject storeButton1;
    public GameObject storeButton2;
    public GameObject storeButton3;

    void Start () {
		if(GameControl.control.adsDisabled)
        {
            storeButton1.SetActive(false);
            storeButton2.SetActive(false);
            storeButton3.SetActive(false);
        }
    }
}
