using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeUI : MonoBehaviour
{

//#if UNITY_IOS

    //Button button;
    float height;
    float width;
    float x;

    void Start()
    {
        height = Screen.height;
        width = Screen.width;

        if (width / height > 0.6f)
        {
            x = (height / width) / 2;
        }
        else
        {
            x = 1;
        }

        this.GetComponent<RectTransform>().localScale = new Vector3(x, x, 1);

    }

    private void Update()
    {

    }
//#endif

}
