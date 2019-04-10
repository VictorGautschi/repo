using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


public class LevelManager : MonoBehaviour
{

    private AdManager adManager;

    public float autoLoadNextLevelAfter; // This is time to next level, if nt 0 then will change level/scene automatically

    public static LevelManager levelManager;

    public static LevelManager Instance()
    {
        if (!levelManager)
        {
            levelManager = FindObjectOfType(typeof(LevelManager)) as LevelManager;
            if (!levelManager)
            {
                Debug.LogError("There needs to be one active LevelManager script on a GameObject in your scene.");
            }
        }
        return levelManager;
    }

    void Start()
    {
        if (autoLoadNextLevelAfter <= 0)
        {
            //Debug.Log ("Level auto load disabled");
        }
        else
        {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        adManager = AdManager.Instance();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            if (SceneManager.GetActiveScene().name == "01 Start")
            {
                QuitRequest();
            }

            if (SceneManager.GetActiveScene().name == "03 Credits" || 
                SceneManager.GetActiveScene().name == "04 LeaderBoard" || 
                SceneManager.GetActiveScene().name == "02b Therapy_Mode" || 
                SceneManager.GetActiveScene().name == "05 Store")
            {
                SceneManager.LoadSceneAsync("01 Start");
            }
        }
    }

    public void LoadLevel(string sceneName)
    {
        //Debug.Log("Level load requested for: " + name);
        //SceneManager.LoadScene(name);
        SceneManager.LoadSceneAsync(sceneName);

        if (sceneName != "03 Credits" &&
            sceneName != "01 Start" &&
            sceneName != "02b Therapy_Mode" &&
            sceneName != "04 LeaderBoard" &&
            sceneName != "05 Store" &&
            !GameControl.control.adsDisabled)
            adManager.ShowAd();
    }

    public void QuitRequest()
    {
        //Debug.Log("I wanna quit");
        Application.Quit();
    }

    public void DefaultRequest()
    {
        //Debug.Log("Default Pressed");
    }

    public void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        //Will load the next level in the sequence (build settings)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadSceneAsync(sceneIndex);

        //if (SceneManager.GetSceneByBuildIndex(sceneIndex) != SceneManager.GetSceneByName("01 Start") &&
        //    SceneManager.GetSceneByBuildIndex(sceneIndex) != SceneManager.GetSceneByName("02b Therapy_Mode") && 
        //    SceneManager.GetSceneByBuildIndex(sceneIndex) != SceneManager.GetSceneByName("03 Credits") &&
        //    SceneManager.GetSceneByBuildIndex(sceneIndex) != SceneManager.GetSceneByName("04 LeaderBoard") &&
        //    !GameControl.control.adsDisabled)
        //{
        //    adManager.ShowAd();
        //}

        /* to do: Show ad here too but make sure only for the right scenes */
    }

    //public void LoadPreviousLevel()
    //{
    //    //Will load the next level in the sequence (build settings)
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    //}

    public void LoadThisLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync(sceneName);

        if (sceneName != "03 Credits" && 
            sceneName != "01 Start" && 
            sceneName != "02b Therapy_Mode" && 
            sceneName != "04 LeaderBoard" && 
            sceneName != "05 Store" &&
            !GameControl.control.adsDisabled)
            adManager.ShowAd();
    }

    //public void WinLevel()
    //{
    //    //Debug.Log("levelReached: " + GameControl.control.levelReached);
    //    //Debug.Log("BuildIndex: " + SceneManager.GetActiveScene().buildIndex);

    //    if (GameControl.control.levelReached == SceneManager.GetActiveScene().buildIndex - 2)
    //    { // 2 because levels start at 3 onwards
    //        GameControl.control.levelReached += 1;
    //        GameControl.control.Save();
    //    }

    //    // Reset for testing
    //    //      GameControl.control.levelReached = 0;
    //    //      GameControl.control.Save();
    //}
}

