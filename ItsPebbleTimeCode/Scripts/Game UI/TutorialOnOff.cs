using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOnOff : MonoBehaviour {

    // This is to make sure the tutorial screen only shows the first time you play the game and otherwise there is a normal start screen that shows before the game

    public GameObject tutorialPanel;
    public GameObject startPanel;

	private void Start () {
        GameControl.control.Load();
        if(GameControl.control.firstTime){
            tutorialPanel.gameObject.SetActive(true);
            // startPanel.gameObject.SetActive(false);

            GameControl.control.firstTime = false;
            GameControl.control.Save();
        } else {
            tutorialPanel.gameObject.SetActive(false);
            startPanel.gameObject.SetActive(true);
        }
	}

    public void OpenTutorial()
    {
        tutorialPanel.gameObject.SetActive(true);
    }
}
