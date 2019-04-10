using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProgressTimeBar : MonoBehaviour
{

    Image progressBarImage;
    TimeManager timeManager;

    //Hashtable param = new Hashtable();

    public float value;
    //{
    //    get
    //    {
    //        if (progressBarImage != null)
    //            return (progressBarImage.fillAmount * 100f);
    //        else
    //            return 0;
    //    }
    //    set
    //    {
    //        if (progressBarImage != null)
    //            progressBarImage.fillAmount = value / 100f;
    //    }
    //}

    private void Awake()
    {
        timeManager = TimeManager.Instance();
    }

    private void Start()
    {
        progressBarImage = gameObject.GetComponent<Image>();
        //Value = timeManager.maxTime / timeManager.totalTime1;
    }

    private void Update()
    {
        value = timeManager.totalTime1 / timeManager.maxTime;

        progressBarImage.fillAmount = value / 1f;
    }

    ////Testing: this function will be called when Test Button is clicked
    //public void UpdateProgress()
    //{
    //    Hashtable param = new Hashtable();
    //    param.Add("from", 0.0f);
    //    param.Add("to", 100);
    //    param.Add("time", 5.0f);
    //    param.Add("onupdate", "TweenedSomeValue");
    //    param.Add("onComplete", "OnFullProgress");
    //    param.Add("onCompleteTarget", gameObject);
    //    iTween.ValueTo(gameObject, param);
    //}

    //public void TweenedSomeValue(int val)
    //{
    //    Value = val;
    //}

    //public void OnFullProgress()
    //{
    //    Value = 0;
    //}
}