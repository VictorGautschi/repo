using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidiOSDifferences : MonoBehaviour {

    public Text iOSText;
    public Text androidText;

    void Start ()
    {

#if UNITY_ANDROID
        iOSText.gameObject.SetActive(false);
        androidText.gameObject.SetActive(true);
#else
        iOSText.gameObject.SetActive(true);
        androidText.gameObject.SetActive(false);
#endif
    }
}
