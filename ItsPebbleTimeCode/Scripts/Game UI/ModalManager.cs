using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModalManager : MonoBehaviour
{
    
    private LosePanel losePanel;
    //private DisplayManager displayManager;
    private LevelManager levelManager;
    private ScoreManager scoreManager;

    private UnityAction myRestartAction;
    private UnityAction myQuitAction;
    //private UnityAction myCancelAction;
    //private UnityAction myNextLevelAction;

    private int bestBestScore;

    public AudioClip modalAudio;

    //private float originalTimeScale;

    private static ModalManager modalManager;

    public static ModalManager Instance()
    {
        if (!modalManager)
        {
            modalManager = FindObjectOfType(typeof(ModalManager)) as ModalManager;
            if (!modalManager)
            {
                Debug.LogError("There needs to be one active ModalManager script on a GameObject in your scene.");
            }
        }
        return modalManager;
    }

    void Awake()
    {
        losePanel = LosePanel.Instance();
        //displayManager = DisplayManager.Instance();
        levelManager = LevelManager.Instance();
        scoreManager = ScoreManager.Instance();

        myRestartAction = new UnityAction(RestartLevel);
        myQuitAction = new UnityAction(QuitToMainMenu);
        //myCancelAction = new UnityAction(Cancel);
        //myNextLevelAction = new UnityAction(NextLevel);
    }

    //// Send to the Modal Panel to set up the Buttons and Functions to call
    //public void RestartQuitCancel()
    //{
    //    originalTimeScale = Time.timeScale;

    //    Time.timeScale = 0;
    //    menuPanel.MenuChoice("What would you like to do?", myRestartAction, myQuitAction, myCancelAction);
    //}

    //// Send to the Modal Panel to set up the Buttons and Functions to call - used in EnemyCountManager Script.
    //public void RestartQuitNext()
    //{
    //    originalTimeScale = Time.timeScale;
    //    //Time.timeScale = 0;
    //    winPanel.WinChoice("VICTORY!", myRestartAction, myQuitAction, myNextLevelAction);
    //    scoreManager.DisplayEndScore();
    //    GameControl.control.Save();
    //}

    // Send to the Modal Panel to set up the Buttons and Functions to call 
    public void RestartQuit()
    {
        GameControl.control.Load();
        if (scoreManager.totalScore > GameControl.control.bestScore)
        {
            GameControl.control.bestScore = scoreManager.totalScore;
            GameControl.control.Save();
        }

        //Debug.Log(" GmaeComtroll " + GameControl.control.bestScore);
        //Debug.Log(" HSController " + HSController.Instance.currentHighScore);


        //if (HSController.Instance.currentHighScore > GameControl.control.bestScore){
        //    bestBestScore = HSController.Instance.currentHighScore;
        //} else {
            bestBestScore = GameControl.control.bestScore;
        //}

        //originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        losePanel.LoseChoice("Score: " + scoreManager.totalScore.ToString(), "Your Best: " + bestBestScore.ToString(), myRestartAction, myQuitAction);
    }

    // These are wrapped into UnityActions
    public void RestartLevel()
    {
        //Time.timeScale = 1;
        levelManager.LoadThisLevel();
        scoreManager.ResetScore();
        //displayManager.DisplayMessage("Restart level");
    }

    public void QuitToMainMenu()
    {
        //Time.timeScale = 1;
        levelManager.LoadLevel("01 Start");
        //displayManager.DisplayMessage("Quit to Start");
        //GameControl.control.Save();
    }

    //void Cancel()
    //{
    //    if (originalTimeScale >= 1)
    //    {
    //        Time.timeScale = originalTimeScale;
    //    }
    //    else
    //    {
    //        Time.timeScale = 0;
    //    }
    //    //Time.timeScale = originalTimeScale;
    //    displayManager.DisplayMessage("Cancelled");
    //}

    //public void NextLevel()
    //{
    //    Time.timeScale = 1;
    //    levelManager.LoadNextLevel();
    //    displayManager.DisplayMessage("Next Level");
    //}
}

