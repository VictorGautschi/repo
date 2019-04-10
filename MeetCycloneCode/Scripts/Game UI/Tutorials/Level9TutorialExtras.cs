using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9TutorialExtras : MonoBehaviour {

    public GameObject energySpeechBubble;

    bool timeStartOff;
    bool timeStartOn;

    float timeShowBubble;
    float timeStopBubble;

	void Start () {
        timeStartOn = true;
        timeStartOff = true;
        timeShowBubble = 105f;
        timeStopBubble = timeShowBubble + 8f;

	}
	
	void Update () {
        if(Time.time >= timeShowBubble && timeStartOn){
            energySpeechBubble.SetActive(true);

            timeStartOn = false;
        }

        if(Time.time >= timeStopBubble && timeStartOff){
            energySpeechBubble.SetActive(false);

            timeStartOff = false;
        }
	}
}
