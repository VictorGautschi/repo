using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public float fadeInTime = 2f;
	private Image fadePanel;
	private Color currentColor = Color.black;
	
	void Start() {
		fadePanel = gameObject.GetComponent<Image>();	
	}
	
	void Update(){
		if (Time.timeSinceLevelLoad < fadeInTime){
			float alphaChange = Time.deltaTime / fadeInTime; //proportion of the fade per frame
			currentColor.a -= alphaChange;
			fadePanel.color = currentColor;
		} else {
			gameObject.SetActive(false);
		}
	}
}
