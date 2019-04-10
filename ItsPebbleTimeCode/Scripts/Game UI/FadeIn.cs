using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public float fadeInTime = 2f;
    private Image fadePanel;
    private Color currentColor = Color.black;

    void Start()
    {
        fadePanel = gameObject.GetComponent<Image>();

        // When coming back from Game Mode the game is paused. So we need to unpause so we can fade the panel again.
        if (Time.timeScale <= 0f)
        {
            Time.timeScale = 1f;
        }
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad < fadeInTime)
        {
            float alphaChange = Time.deltaTime / fadeInTime; //proportion of the fade per frame
            currentColor.a -= alphaChange;
            fadePanel.color = currentColor;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

