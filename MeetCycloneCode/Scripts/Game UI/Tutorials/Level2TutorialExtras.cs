using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2TutorialExtras : MonoBehaviour
{
    bool upgradeable;

    public GameObject holdSpeechBubble;

    public int upgradeCost = 30; // hard coded for now
    private CreditManager creditManager;

    private float fadeInTime = 3f;
    private float fadeOutTime = 6f;

    private Color currentColor = Color.white;

    void Update()
    {
        if (Time.timeSinceLevelLoad > fadeInTime && Time.timeSinceLevelLoad < fadeOutTime)
            holdSpeechBubble.gameObject.SetActive(true);
        {

            if (Time.timeSinceLevelLoad >= fadeOutTime)
            {
                float alphaChange = Time.deltaTime / fadeOutTime; //proportion of the fade per frame
                currentColor.a += alphaChange;
                holdSpeechBubble.GetComponent<Image>().color = currentColor;

                if (Time.timeSinceLevelLoad >= (fadeOutTime + 3f))
                {
                    Destroy(holdSpeechBubble);
                    Destroy(gameObject);
                }
            }
        }
    }
}