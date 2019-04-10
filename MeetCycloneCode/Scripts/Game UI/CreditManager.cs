using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour {

	/*
		Credits are subtracted in the UpgradeManager Script.
		Credits are added in the EnemySpawner Script.
		The ConfirmPanel will only appear when there is enough credit for the respective upgrade
	*/

	public Text creditText;
	private int startingCredit = 0;
	public int credit;

	private static CreditManager creditManager;

	public static CreditManager Instance () {
		if (!creditManager) {
			creditManager = FindObjectOfType(typeof (CreditManager)) as CreditManager;
			if (!creditManager){
				Debug.LogError ("There needs to be one active CreditManager script on a GameObject in your scene.");
			}
		}
		return creditManager;
	}

	void Start() { 
		credit = startingCredit;
		DisplayCredit ();
	}

	public void AddCredit (int _credit) {
		credit += _credit;
		DisplayCredit ();
	}

	public void SubtractCredit (int _credit) {
		if (credit >= _credit){
			credit -= _credit;
		}
		DisplayCredit ();
	}

	void DisplayCredit () {
		creditText.text = credit.ToString();
	}
}
