using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoOnOff : MonoBehaviour {

    public GameObject video;
    public VideoPlayer videoPlayer;
    public Button videoButton;

    bool videoReadyCheck;
    bool firstTimeVariable; // This is to check whether this is the first time before it is set somewhere else

    private void Awake()
    {
        firstTimeVariable = GameControl.control.firstTime;
    }

    private void Update()
    {
        if(videoReadyCheck == false)
        {
            if (videoPlayer.isPrepared)
            {
                videoButton.gameObject.SetActive(true);

                if (firstTimeVariable)
                {
                    video.SetActive(true);
                } 
                else 
                {
                    video.SetActive(false);
                }
                videoReadyCheck = true;
            }
        }
    }

    public void VideoToggle()
    {
        
        if (video.activeInHierarchy)
        {
            video.SetActive(false);
        }
        else
        {
            video.SetActive(true);
        }
    }
}
