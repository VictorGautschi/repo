using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraInfoManager : MonoBehaviour {

	public Image upgradeInfoWindow;
	public Text headingText;
	public string heading;
	public Text bodyText;
	public string body;

    Vector3 originalWindowPosition;

	private float fadeTime = 0.5f;
	Color resetWindowColor;
	Color resetHeadingColor;
	Color resetBodyColor;
	Color resetValueColor;

	private IEnumerator fadeAlpha;

    private void Start()
    {
        originalWindowPosition = upgradeInfoWindow.transform.position;
    }

    public void InfoWindowDisappear() {
        upgradeInfoWindow.transform.position = originalWindowPosition;

        // From before
        //resetWindowColor = upgradeInfoWindow.color;
		//resetWindowColor.a = 0;
		//upgradeInfoWindow.color = resetWindowColor;

		//resetHeadingColor = headingText.color;
		//resetHeadingColor.a = 0;
		//headingText.color = resetHeadingColor;

		//resetBodyColor = bodyText.color;
		//resetBodyColor.a = 0;
		//bodyText.color = resetBodyColor;
	}

	public void DisplayInfoWindow () {
        
		upgradeInfoWindow.transform.position = transform.position;
		SetAlpha();
		headingText.text = heading;
		bodyText.text = body;
	}

	void SetAlpha () {
		if (fadeAlpha != null) {
			StopCoroutine(fadeAlpha);
		}

		fadeAlpha = FadeAlphaIn ();
		StartCoroutine (fadeAlpha);
	}

	IEnumerator FadeAlphaIn(){
		resetWindowColor = upgradeInfoWindow.color;
		resetWindowColor.a = 0;
		upgradeInfoWindow.color = resetWindowColor;

		resetHeadingColor = headingText.color;
		resetHeadingColor.a = 0;
		headingText.color = resetHeadingColor;

		resetBodyColor = bodyText.color;
		resetBodyColor.a = 0;
		bodyText.color = resetBodyColor;

		yield return new WaitForSeconds(0.7f);

        while (Input.GetMouseButton(0)) {
			Color displayWindowColor = upgradeInfoWindow.color;
			displayWindowColor.a += Time.deltaTime / fadeTime;
			upgradeInfoWindow.color = displayWindowColor;

			Color displayHeadingColor = headingText.color;
			displayHeadingColor.a += Time.deltaTime / fadeTime;
			headingText.color = displayHeadingColor;

			Color displayBodyColor = bodyText.color;
			displayBodyColor.a += Time.deltaTime / fadeTime;
			bodyText.color = displayBodyColor;

			yield return null;
		}
		yield return null;
	} 
}
