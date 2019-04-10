using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URLLinks : MonoBehaviour {

    public void OpenURLTwitter()
    {
        Application.OpenURL("");
    }

    public void OpenURLFaceBook()
    {
        Application.OpenURL("");
    }

    public void OpenURLUnityPrivacyPolicy()
    {
        Application.OpenURL("https://unity3d.com/legal/privacy-policy");
    }

}
