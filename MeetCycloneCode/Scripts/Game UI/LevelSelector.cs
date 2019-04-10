using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Dreamteck.UI;

public class LevelSelector : MonoBehaviour {

	public GameObject[] chapters;
	public GameObject settings;
	public Button[] levelButtons;

    private Animator chapter1Animator;
    private Animator chapter2Animator;
    private Animator chapter3Animator;

    bool chapter1IsCentre = true;
    bool chapter2IsCentre = false;
    bool chapter3IsCentre = false;

    int count;

	GameObject go;

    private void Awake()
    {
        /* To set for testing */
        //GameControl.control.adsDisabled = false;
        //GameControl.control.levelReached = 21;
        //GameControl.control.Save();   

        GameControl.control.Load();
    }

    void Start() {

        chapter1Animator = chapters[0].GetComponent<Animator>();
        chapter2Animator = chapters[1].GetComponent<Animator>();
        chapter3Animator = chapters[2].GetComponent<Animator>();

		//GameControl.control.Load();

        int levelReached = GameControl.control.levelReached;
        // int levelReached = 22; // for testing
        int[] numberStarsPerLevel = GameControl.control.numberStarsPerLevel;

		if (levelReached == 0) {
			levelReached = 1;
			GameControl.control.levelReached = 1;
		}

        if(levelReached == 20 && numberStarsPerLevel[19] > 0){
            levelReached = 21;
            GameControl.control.levelReached = 21;
        }

        /* Resize both the variable and the saved variable if not already so */
        if (levelReached > numberStarsPerLevel.Length)
        {
            System.Array.Resize<int>(ref numberStarsPerLevel, levelReached);
            System.Array.Resize<int>(ref GameControl.control.numberStarsPerLevel, levelReached);
            //GameControl.control.numberStarsPerLevel[levelReached - 1] = 0; 

            //Debug.Log("levelreached " + levelReached);

        }

        //foreach(int xx in numberStarsPerLevel){
        //    count += 1;
        //    Debug.Log(xx);
        //}

        //Debug.Log("count " + count);
        //Debug.Log("numberStars " + numberStarsPerLevel.Length);
        //Debug.Log("sdf " + numberStarsPerLevel[21]);


		for (int i = 0; i < levelButtons.Length ; i++) {
			if (i + 1 > levelReached) {
				levelButtons[i].interactable = false;		
				GameObject go0 = levelButtons[i].gameObject.transform.GetChild(0).gameObject;
				go0.SetActive(true);
				GameObject go1 = levelButtons[i].gameObject.transform.GetChild(1).gameObject;
				go1.SetActive(false);
			}
		}

		for (int i = 0; i < levelReached ; i++) {
			ProgressGroup pg = levelButtons[i].GetComponentInChildren<ProgressGroup>();
			pg.progress = numberStarsPerLevel[i];
		}
	}

    public void NextChapter() {
        
        if (chapter1IsCentre)
        {
            chapter1Animator.SetTrigger("moveLeft");
            chapter1IsCentre = false;
            chapter2Animator.SetTrigger("moveCentre");
            chapter2IsCentre = true;
        }
        else if (chapter2IsCentre)
        {
            chapter2Animator.SetTrigger("moveLeft");
            chapter2IsCentre = false;
            chapter3Animator.SetTrigger("moveCentre");
            chapter3IsCentre = true;
        }
	}

	public void PreviousChapter() {

        if (chapter3IsCentre)
        {
            chapter3Animator.SetTrigger("moveRight");
            chapter3IsCentre = false;
            chapter2Animator.SetTrigger("moveCentre");
            chapter2IsCentre = true;
        }
        else if (chapter2IsCentre)
        {
            chapter2Animator.SetTrigger("moveRight");
            chapter2IsCentre = false;
            chapter1Animator.SetTrigger("moveCentre");
            chapter1IsCentre = true;
        }
	}

	public void Settings() {
        settings.gameObject.SetActive(true);
	}

	public void exitSettings() {
		settings.gameObject.SetActive(false);
	}
}
