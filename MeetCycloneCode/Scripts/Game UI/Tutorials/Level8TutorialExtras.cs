using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8TutorialExtras : MonoBehaviour {

    public GameObject useHealthSpeechBubble;

    public GameObject healthPickup;

    Ship ship;

    float timeStart;
    bool allActive;
    bool timeStartOff;

    void Start()
    {
        allActive = true;
        timeStartOff = true;
        timeStart = 150f;
        ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
        ship.health.value -= 0.3f;
    }

    void Update()
    {
        if (healthPickup == null && allActive)
        {

            useHealthSpeechBubble.SetActive(true);

            if (timeStartOff)
            {
                timeStart = Time.time + 5f;

                timeStartOff = false;
            }

            if (Time.time > timeStart)
            {
                useHealthSpeechBubble.SetActive(false);

                allActive = false;
            }
        }
    }
}
