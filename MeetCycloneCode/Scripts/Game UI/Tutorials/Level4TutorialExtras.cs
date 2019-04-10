using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4TutorialExtras : MonoBehaviour {

    public GameObject energySpeechBubble;
    public GameObject useEnergySpeechBubble;

    public GameObject energyCapsule;

    Player player;

    float timeStart;
    bool allActive;
    bool timeStartOff;

	void Start () {
        allActive = true;
        timeStartOff = true;
        timeStart = 150f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.energy.value -= 0.5f;
	}
	
	void Update () {
        if (energyCapsule == null && allActive){

            useEnergySpeechBubble.SetActive(true);

            if(timeStartOff){
                timeStart = Time.time + 5f;

                timeStartOff = false;
            }

            if (Time.time > timeStart)
            {
                useEnergySpeechBubble.SetActive(false);

                allActive = false;
            }
        }
	}
}
