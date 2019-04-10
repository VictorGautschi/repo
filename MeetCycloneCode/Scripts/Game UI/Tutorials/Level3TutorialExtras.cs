using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3TutorialExtras : MonoBehaviour {

    public GameObject energySpeechBubble;

    float energyLabelOnTime;
    float energyLabelOffTime;

    bool energyNotYetActive;
    bool energyActive;

	void Start () {
        energyActive = true;
        energyNotYetActive = true;
        energyLabelOnTime = Time.time + 8f;
        energyLabelOffTime = energyLabelOnTime + 8f;
	}
	
	void Update () {
        if (Time.time > energyLabelOnTime && energyNotYetActive)
        {
            energySpeechBubble.SetActive(true);

            energyNotYetActive = false;
        }

        if (Time.time > energyLabelOffTime && energyActive)
        {
            energySpeechBubble.SetActive(false);

            energyActive = false;
        }
	}
}
