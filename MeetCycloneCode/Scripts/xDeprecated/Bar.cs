using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

	private float fillAmount;

	[SerializeField]
	private float lerpSpeed;

	[SerializeField]
	private Image content;

	[SerializeField]
	private Text valueText;

	[SerializeField]
	private Color fullColor;

	[SerializeField]
	private Color lowColor;

	[SerializeField]
	private bool lerpColors;

	public float MaxValue { get; set; }

	public float Value { // this updates the fillAmount above as fillAmount changes
		set {
			string[] tmp = valueText.text.Split(':');
			valueText.text = tmp[0] + ": " + Mathf.RoundToInt(value) + "/" + Mathf.RoundToInt(MaxValue);
			fillAmount = Map(value, 0, MaxValue, 0, 1);
		}
	}

	void Start () {
		if (lerpColors) {
			content.color = fullColor;
		}
	}

	void Update () {
		HandleBar();
	}

	private void HandleBar() {
		if (fillAmount != content.fillAmount) { // update only if it changes
			content.fillAmount = Mathf.Lerp(content.fillAmount,fillAmount,Time.deltaTime * lerpSpeed);
		}

		if (lerpColors) {
			content.color = Color.Lerp(lowColor, fullColor, fillAmount);
		}
	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax){
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	} 
}



