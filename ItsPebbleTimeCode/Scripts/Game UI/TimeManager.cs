using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    
    public Text displayTime1;
    //public Text displayTime2;

    public Text timeUpText;

    public float totalTime1;
    //float totalTime2;

    public float maxTime = 15;
    public ParticleSystem timeUpEffect;
    public GameObject ball;

    bool unpaused = false;

    [HideInInspector]
    public bool ballDestroyed = false;

    float timeSinceDie;
    float timeBeforeLoseScreen = 2f;

    ModalManager modalManager;
    DisplayManager displayManager;

    private static TimeManager timeManager;

    public static TimeManager Instance()
    {
        if (!timeManager)
        {
            timeManager = FindObjectOfType(typeof(TimeManager)) as TimeManager;
            if (!timeManager)
            {
                Debug.LogError("There needs to be one active TimeManager script on a GameObject in your scene.");
            }
        }
        return timeManager;
    }

    private void Awake()
    {
        modalManager = ModalManager.Instance();
        displayManager = DisplayManager.Instance();
        StopTime();
    }

    private void Start()
    {
        totalTime1 = maxTime;
        //totalTime2 = maxTime;
        displayTime1.text = totalTime1.ToString();
        //displayTime2.text = totalTime2.ToString();
    }

    private void Update()
    {
        if(unpaused == true){
            totalTime1 -= Time.deltaTime;
            //totalTime2 -= Time.deltaTime;

            if (totalTime1 > maxTime)
            {
                totalTime1 = maxTime;
            }

            //if (totalTime2 > maxTime)
            //{
            //    totalTime2 = maxTime;
            //}

            if (totalTime1 <= 0) //|| totalTime2 <= 0)
            {
                if(!ballDestroyed){
                    Instantiate(timeUpEffect.gameObject, ball.transform.position, ball.transform.rotation);
                    Destroy(ball.gameObject);
                    ballDestroyed = true;
                    displayManager.DisplayTimeUp();
                }


                // Wait just a little before showing the modal screen, so we can show the particle effect
                timeSinceDie += Time.deltaTime;
                if (timeSinceDie > timeBeforeLoseScreen)
                {
                    modalManager.RestartQuit();
                    unpaused = false;
                }
            }
            displayTime1.text = totalTime1.ToString();
            // displayTime2.text = totalTime2.ToString();
        }
    }

    public void AddTime(float time, string blueRed)
    {
        //if (blueRed == "RED")
        //{
        //    totalTime1 += time;
        //    //Debug.Log("totalTime " + totalTime1);
        //    displayTime1.text = totalTime1.ToString();
        //}    
        //else if(blueRed == "BLUE") 
        //{
        //    totalTime2 += time;
        //    //Debug.Log("totalTime " + totalTime2);
        //    displayTime2.text = totalTime2.ToString();
        //} 
        //else 
        //{
            // This else deals with the timeBombs - so time down for both
            totalTime1 += time;
            //Debug.Log("totalTime " + totalTime1);
            displayTime1.text = totalTime1.ToString();
        //    totalTime2 += time;
        //    //Debug.Log("totalTime " + totalTime2);
        //    displayTime2.text = totalTime2.ToString();
        //}
    }

    public void StartTime()
    {
        Time.timeScale = 1;
        unpaused = true;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
        unpaused = false;
    }
}
