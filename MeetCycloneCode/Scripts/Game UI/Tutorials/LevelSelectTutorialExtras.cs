using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectTutorialExtras : MonoBehaviour {

    public GameObject selectLevelSpeechBubble;
    public GameObject playSpeechBubble;
    public GameObject selectNextChapterSpeechBubble;
    public Button startButton;

    int[] numberStarsPerLevel;

    private bool level1Selected = false;

    private void Start()
    {
        GameControl.control.Load();
        numberStarsPerLevel = GameControl.control.numberStarsPerLevel;

       // Debug.Log(numberStarsPerLevel[0]);
    }

    private void Update()
    {
        if (numberStarsPerLevel[0] <= 0)
        {

            if (startButton.interactable == true)
            {
                level1Selected = true;
            }
            else
            {
                level1Selected = false;
            }

            if (!level1Selected)
            {
                selectLevelSpeechBubble.SetActive(true);
            }
            else
            {
                playSpeechBubble.SetActive(true);
                selectLevelSpeechBubble.SetActive(false);
            }
        }
        else
        {
            selectLevelSpeechBubble.SetActive(false);
            playSpeechBubble.SetActive(false);
        }
    
        if (numberStarsPerLevel[7] > 0 && numberStarsPerLevel[8] <= 0){
            selectNextChapterSpeechBubble.SetActive(true);
        } else {
            selectNextChapterSpeechBubble.SetActive(false);
        }   
    }

}
