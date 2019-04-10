using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DisableSecondaryWeaponButton : MonoBehaviour {

	[SerializeField]
	private Button disableSecondaryButton;
	[SerializeField]
	private Image disabledImage;
	[SerializeField]
	private Text disabledText;

	GameObject secondaryWeapon;
	Color originalColor;

	private float startTimeOfPressingButton;

	bool weaponDisabled;

	public static DisableSecondaryWeaponButton disableSecondaryPanel;

	public static DisableSecondaryWeaponButton Instance () {
		if (!disableSecondaryPanel) {
			disableSecondaryPanel = FindObjectOfType(typeof (DisableSecondaryWeaponButton)) as DisableSecondaryWeaponButton;
			if (!disableSecondaryPanel){
				Debug.LogError ("There needs to be one active DisableSecondaryWeaponButton script on a GameObject in your scene.");
			}
		}
		return disableSecondaryPanel;
	}

	void Awake () {
		secondaryWeapon = GameObject.FindGameObjectWithTag("Secondary Weapon");
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			startTimeOfPressingButton = Time.time;
		}	
	}

	public void DisableSecondaryWeapon () {
		weaponDisabled = secondaryWeapon.GetComponent<ShootAttack>().weaponDisabled;

		if((Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			if (!weaponDisabled) {
				secondaryWeapon.GetComponent<ShootAttack>().weaponDisabled = true;
				disabledImage.enabled = true;
				disabledText.text = "DISABLED";
			} else {
				secondaryWeapon.GetComponent<ShootAttack>().weaponDisabled = false;
				disabledImage.enabled = false;
				disabledText.text = "ENABLED";
			}
		}
	}
}
