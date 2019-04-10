using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {

	public Text displayScore;
    public Text timesUpText;
    private ScoreManager scoreManager;

	public float displayTimeDelay;
	public float fadeTime;

	private IEnumerator fadeAlpha;
    private IEnumerator fadeAlphaTimeUp;

	private static DisplayManager displayManager;

	public static DisplayManager Instance () {
		if (!displayManager) {
			displayManager = FindObjectOfType(typeof (DisplayManager)) as DisplayManager;
			if (!displayManager){
				Debug.LogError ("There needs to be one active DisplayManager script on a GameObject in your scene.");
			}
		}
		return displayManager;
	}

    private void Awake()
    {
        scoreManager = ScoreManager.Instance();
        scoreManager.AddPoints += DisplayScore; 
    }

    public void DisplayScore () {
        if (displayScore != null) {
            displayScore.text = scoreManager.totalScore.ToString();
            SetAlpha();
        }
	}

	void SetAlpha () {
		if (fadeAlpha != null) {
			StopCoroutine(fadeAlpha);
		}

		fadeAlpha = FadeAlpha ();
		StartCoroutine (fadeAlpha);
	}

	IEnumerator FadeAlpha() {
        Color resetColor = displayScore.color;
		resetColor.a = 1;
        displayScore.color = resetColor;

		yield return new WaitForSeconds (displayTimeDelay);

        while (displayScore.color.a > 0) {
            Color displayColor = displayScore.color;
			displayColor.a -= Time.deltaTime / fadeTime;
            displayScore.color = displayColor;
			yield return null;
		}	
		yield return null;
	}



    public void DisplayTimeUp()
    {
        if (timesUpText != null)
        {
            timesUpText.text = "Time Up!";
            SetAlphaTimeUp();
        }
    }

    void SetAlphaTimeUp()
    {
        if (fadeAlphaTimeUp != null)
        {
            StopCoroutine(fadeAlphaTimeUp);
        }

        fadeAlphaTimeUp = FadeAlphaTimeUp();
        StartCoroutine(fadeAlphaTimeUp);
    }

    IEnumerator FadeAlphaTimeUp()
    {
        Color resetColor = timesUpText.color;
        resetColor.a = 1;
        timesUpText.color = resetColor;

        yield return new WaitForSeconds(displayTimeDelay);

        while (timesUpText.color.a > 0)
        {
            Color timeUpColor = timesUpText.color;
            timeUpColor.a -= Time.deltaTime / 1f;
            timesUpText.color = timeUpColor;
            yield return null;
        }
        yield return null;
    }
}
