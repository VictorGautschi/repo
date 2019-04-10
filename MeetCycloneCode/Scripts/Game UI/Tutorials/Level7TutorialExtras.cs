using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7TutorialExtras : MonoBehaviour {
    
    public GameObject useBlastSpeechBubble;

    public GameObject lightningBlast;

    float timeStart;
    bool allActive;
    bool timeStartOff;

    void Start()
    {
        allActive = true;
        timeStartOff = true;
        timeStart = 150f;
    }

    void Update()
    {
        if (lightningBlast == null && allActive)
        {

            useBlastSpeechBubble.SetActive(true);

            if (timeStartOff)
            {
                timeStart = Time.time + 5f;

                timeStartOff = false;
            }

            if (Time.time > timeStart)
            {
                useBlastSpeechBubble.SetActive(false);

                allActive = false;
            }
        }
    }
}
