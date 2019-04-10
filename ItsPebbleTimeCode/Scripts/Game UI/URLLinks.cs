using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URLLinks : MonoBehaviour
{

    public void OpenURLPrivacyPolicy()
    {
        Application.OpenURL("https://unity3d.com/legal/privacy-policy");
    }

    public void OpenURLFacebook()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        if (checkPackageAppIsPresent("com.facebook.katana"))
        {
            Application.OpenURL("fb://page/341323459765277"); //there is Facebook app installed so let's use it
        }
        else
        {
            Application.OpenURL("https://web.facebook.com/Vormike-Studios-341323459765277"); // no Facebook app - use built-in web browser
        }
#else
        OpenFacebookPage();
#endif

    }

    void OpenFacebookPage()
    {
        float startTime;
        startTime = Time.timeSinceLevelLoad;

        //open the facebook app
        Application.OpenURL("fb://profile/341323459765277");

        if (Time.timeSinceLevelLoad - startTime <= 1f)
        {
            //fail. Open safari.
            Application.OpenURL("https://web.facebook.com/Vormike-Studios-341323459765277");
        }
    }

    private bool checkPackageAppIsPresent(string package)
    {
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        //take the list of all packages on the device
        AndroidJavaObject appList = packageManager.Call<AndroidJavaObject>("getInstalledPackages", 0);
        int num = appList.Call<int>("size");
        for (int i = 0; i < num; i++)
        {
            AndroidJavaObject appInfo = appList.Call<AndroidJavaObject>("get", i);
            string packageNew = appInfo.Get<string>("packageName");
            if (packageNew.CompareTo(package) == 0)
            {
                return true;
            }
        }
        return false;
    }
}
