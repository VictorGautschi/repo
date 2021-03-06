﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCapture : MonoBehaviour
{

    public KeyCode keyToPress = KeyCode.K;
    public int resolutionModifier = 1;
    public string prefix = "ss";

    bool takePicture = false;

    private void Start()
    {
        if (!System.IO.Directory.Exists(Application.dataPath + "/../Screenshots"))
            System.IO.Directory.CreateDirectory(Application.dataPath + "/../Screenshots");
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            takePicture = true;
        }
    }

    private void OnPostRender()
    {
        if (takePicture)
        {
            string dateTime = System.DateTime.Now.Month.ToString() + "-" +
                                System.DateTime.Now.Day.ToString() + "_" +
                                System.DateTime.Now.Hour.ToString() + "-" +
                                System.DateTime.Now.Minute.ToString() + "-" +
                                System.DateTime.Now.Second.ToString();
            string filename = prefix + "_" + dateTime + ".png";
            UnityEngine.ScreenCapture.CaptureScreenshot((Application.dataPath + "/../Screenshots/" + filename), resolutionModifier);
            takePicture = false;

        }
    }
}
