using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using DigitalRuby.ThunderAndLightning;

public class UpgradeManager : MonoBehaviour {

	private bool upgradeable; 
	private Color originalColor;
	private int numLaserGuns;
	private float startTimeOfPressingButton;

	private GameObject playerGameObject;
	private GameObject primaryWeaponGameObject;
	private GameObject secondaryWeaponGameObject;
	private GameObject[] shipWeaponGameObject;
	private GameObject lightningBoltGameObject;
	private GameObject laserBeamGameObject;

	private DisplayManager displayManager;
	private CreditManager creditManager;

	private int upgrade1Level = 1;
	private int upgrade2Level = 1;
	private int upgrade3Level = 1;
	private int upgrade4Level = 1;
	private int upgrade5Level = 1;
	private int upgrade6Level = 1;

	private UnityAction myConfirmAction1;
	private UnityAction myConfirmAction2;
	private UnityAction myConfirmAction3;
	private UnityAction myConfirmAction4;
	private UnityAction myConfirmAction5;
	private UnityAction myConfirmAction6;

	public ConfirmPanel confirmUpgradePanel; // Confirm button object or panel
	public Button upgradeButton;
	public Text nextUpgradeCostText;
	public Text currentUpgradeLevelText;
	public GameObject moneyImage;
	public int maxUpgrade = 10;
    public int upgradeCost = 30;

    private Animator anim;

	void Awake(){
		playerGameObject = GameObject.FindGameObjectWithTag("Player");
        primaryWeaponGameObject = GameObject.FindGameObjectWithTag("Primary Weapon");
		secondaryWeaponGameObject = GameObject.FindGameObjectWithTag("Secondary Weapon");

		lightningBoltGameObject = GameObject.FindGameObjectWithTag("Lightning");
		laserBeamGameObject = GameObject.FindGameObjectWithTag("Laser Beam");

		displayManager = DisplayManager.Instance();
		creditManager = CreditManager.Instance();

		myConfirmAction1 = new UnityAction (UpgradeLightningBolt);
		myConfirmAction2 = new UnityAction (UpgradeChainLightning);
		myConfirmAction3 = new UnityAction (UpgradeCyclonePrimaryWeapon);
		myConfirmAction4 = new UnityAction (UpgradeCycloneSpeed);
		myConfirmAction5 = new UnityAction (UpgradeShipWeapon);
		myConfirmAction6 = new UnityAction (UpgradeEnergyRegeneration);

		originalColor = nextUpgradeCostText.color;

		upgradeButtonCostText (1, maxUpgrade);

        //nextUpgradeCostText.text = upgradeCost.ToString();
	}

	void Start(){
		shipWeaponGameObject = GameObject.FindGameObjectsWithTag("Ship Weapon");
		numLaserGuns = shipWeaponGameObject.Length;
        anim = GetComponent<Animator>();
    }

    void Update () {
		if (nextUpgradeCostText.color == Color.grey){
			upgradeable = false;
            anim.SetTrigger("isIdling");
            anim.ResetTrigger("isGlowing");
		} else {
			upgradeable = true;
		}

		if(upgradeable) {
			if (creditManager.credit < upgradeCost){
				nextUpgradeCostText.color = Color.red;
                anim.ResetTrigger("isGlowing");
                anim.SetTrigger("isIdling");
			} else {
				nextUpgradeCostText.color = originalColor;
                anim.ResetTrigger("isIdling");
                anim.SetTrigger("isGlowing");
			}
		} 

		if (Input.GetMouseButtonDown(0)) {
			startTimeOfPressingButton = Time.time;
		}		
	}

	public void ConfirmUpgradeCycloneLightningBolt () {
		if (creditManager.credit >= upgradeCost && upgradeable && (Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			confirmUpgradePanel.Confirm (myConfirmAction1,this.transform,true);
		} else {
            confirmUpgradePanel.Confirm(myConfirmAction1, this.transform, false);
		}
	}

	public void ConfirmUpgradeCycloneChainLightning () {
		if (creditManager.credit >= upgradeCost && upgradeable && (Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			confirmUpgradePanel.Confirm (myConfirmAction2,this.transform,true);
		} else {
			confirmUpgradePanel.Confirm (myConfirmAction2,this.transform,false);
		}
	}

	public void ConfirmUpgradeCyclonePrimaryWeapon () {
		if (creditManager.credit >= upgradeCost && upgradeable && (Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			confirmUpgradePanel.Confirm (myConfirmAction3,this.transform,true);
		} else {
			confirmUpgradePanel.Confirm (myConfirmAction3,this.transform,false);
		}
	}

	public void ConfirmUpgradeCycloneSpeed () {
		if (creditManager.credit >= upgradeCost && upgradeable && (Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			confirmUpgradePanel.Confirm (myConfirmAction4,this.transform,true);
		} else {
			confirmUpgradePanel.Confirm (myConfirmAction4,this.transform,false);
		}
	}

	public void ConfirmUpgradeShipWeapon () {
		if (creditManager.credit >= upgradeCost && upgradeable && (Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			confirmUpgradePanel.Confirm (myConfirmAction5,this.transform,true);
		} else {
			confirmUpgradePanel.Confirm (myConfirmAction5,this.transform,false);
		}
	}

	public void ConfirmUpgradeEnergyRegeneration () {
		if (creditManager.credit >= upgradeCost && upgradeable && (Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			confirmUpgradePanel.Confirm (myConfirmAction6,this.transform,true);
		} else {
			confirmUpgradePanel.Confirm (myConfirmAction6,this.transform,false);
		}
	}

	void UpgradeLightningBolt () {

		if(upgrade1Level < maxUpgrade){
			upgrade1Level += 1;
		
			displayManager.DisplayMessage ("Lightning Bolt: Level " + upgrade1Level);

			secondaryWeaponGameObject.GetComponentInChildren<ShootAttack>().shootRange = 
				Mathf.RoundToInt(secondaryWeaponGameObject.GetComponentInChildren<ShootAttack>().shootRange * 2f);

			secondaryWeaponGameObject.GetComponentInChildren<LightningWeapon>().damage = 
				Mathf.RoundToInt(secondaryWeaponGameObject.GetComponentInChildren<LightningWeapon>().damage * 2f);

            secondaryWeaponGameObject.GetComponentInChildren<LightningWeapon>().energyCostPerc =
               secondaryWeaponGameObject.GetComponentInChildren<LightningWeapon>().energyCostPerc + 0.005f;

			if(upgrade1Level == 2) {
				lightningBoltGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(1, 0.92f, 0.016f, 1);
			}

			if(upgrade1Level == 3) {
				lightningBoltGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(0, 1, 0, 1);
			}

			if(upgrade1Level == 4) {
				lightningBoltGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(0, 0, 1, 1);
			}

			if(upgrade1Level == 5) {
				lightningBoltGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(1, 0, 1, 1);
			}

			if(upgrade1Level == 6) {
				lightningBoltGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(1, 0, 0, 1);
			}

			creditManager.SubtractCredit(upgradeCost);
			upgradeCost = Mathf.RoundToInt(upgradeCost * 3f); 

			upgradeButtonCostText (upgrade1Level,maxUpgrade);
			currentUpgradeLevelText.text = upgrade1Level.ToString();
		} else {
			upgradeButtonCostText (upgrade1Level,maxUpgrade);
		}
	}

	void UpgradeChainLightning () {
		if(upgrade2Level < maxUpgrade){
			upgrade2Level += 1;
			displayManager.DisplayMessage ("Chain Lightning: Level " + upgrade2Level);

			secondaryWeaponGameObject.GetComponentInChildren<LightningWeapon>().numberOfJumps += 1; 

            secondaryWeaponGameObject.GetComponentInChildren<LightningWeapon>().energyCostPerc =
               secondaryWeaponGameObject.GetComponentInChildren<LightningWeapon>().energyCostPerc + 0.015f;

			creditManager.SubtractCredit(upgradeCost);
			upgradeCost = Mathf.RoundToInt(upgradeCost * 3f);

			upgradeButtonCostText (upgrade2Level,maxUpgrade);
			currentUpgradeLevelText.text = upgrade2Level.ToString();
		} else {
			upgradeButtonCostText (upgrade2Level,maxUpgrade);
		}
	}

	void UpgradeCyclonePrimaryWeapon () {
		if(upgrade3Level < maxUpgrade){
			upgrade3Level += 1;
			displayManager.DisplayMessage ("Machine Gun: Level "  + upgrade3Level);

            primaryWeaponGameObject.GetComponentInChildren<ShootAttack>().shootRange =
                Mathf.RoundToInt(primaryWeaponGameObject.GetComponentInChildren<ShootAttack>().shootRange * 1.166f); // 1.165f before

			primaryWeaponGameObject.GetComponentInChildren<WeaponController>().equippedWeapon.msBetweenShots = 
                Mathf.RoundToInt(primaryWeaponGameObject.GetComponentInChildren<WeaponController>().equippedWeapon.msBetweenShots / 1.4f); // 1.4f before

			creditManager.SubtractCredit(upgradeCost);
			upgradeCost = Mathf.RoundToInt(upgradeCost * 2f);

			upgradeButtonCostText (upgrade3Level,maxUpgrade);
			currentUpgradeLevelText.text = upgrade3Level.ToString();
		} else {
			upgradeButtonCostText (upgrade3Level,maxUpgrade);
		}
	}

	void UpgradeCycloneSpeed () {
		if(upgrade4Level < maxUpgrade){
			upgrade4Level += 1;
			displayManager.DisplayMessage ("Cyclone Speed: Level " + upgrade4Level);

            playerGameObject.GetComponent<PlayerMovement>().speed = playerGameObject.GetComponent<PlayerMovement>().speed + 1; 

			creditManager.SubtractCredit(upgradeCost);
			upgradeCost = Mathf.RoundToInt(upgradeCost * 2f);

			upgradeButtonCostText (upgrade4Level, maxUpgrade);
			currentUpgradeLevelText.text = upgrade4Level.ToString();
		} else {
			upgradeButtonCostText (upgrade4Level, maxUpgrade);
		}
	}

	void UpgradeShipWeapon () {
		if(upgrade5Level < maxUpgrade){
			upgrade5Level += 1;
			displayManager.DisplayMessage ("Ship Weapon: Level "  + upgrade5Level);

			for (int i = 0; i < numLaserGuns; i++) {
                shipWeaponGameObject[i].GetComponentInParent<ShootAttack>().shootRange =
                    Mathf.RoundToInt(shipWeaponGameObject[i].GetComponentInParent<ShootAttack>().shootRange * 1.15f);

				shipWeaponGameObject[i].GetComponent<LaserWeapon>().damage = 
                    shipWeaponGameObject[i].GetComponent<LaserWeapon>().damage + 2;

                //Debug.Log("Ship Laser " + shipWeaponGameObject[i].GetComponent<LaserWeapon>().damage);
                
				shipWeaponGameObject[i].GetComponentInParent<WeaponController>().equippedWeapon.msBetweenShots = 
                    Mathf.RoundToInt(shipWeaponGameObject[i].GetComponentInParent<WeaponController>().equippedWeapon.msBetweenShots / 1.5f);
			}

			if(upgrade5Level == 2) {
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(1, 0.92f, 0.016f, 1);
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().LightningTintColor = new Color(1, 0.92f, 0.016f, 1);	
			}

			if(upgrade5Level == 3) {
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(0, 1, 0, 1);
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().LightningTintColor = new Color(0, 1, 0, 1);
			}

			if(upgrade5Level == 4) {
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(0, 0, 1, 1);
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().LightningTintColor = new Color(0, 0, 1, 1);
			}

			if(upgrade5Level == 5) {
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(1, 0, 1, 1);
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().LightningTintColor = new Color(1, 0, 1, 1);
			}

			if(upgrade5Level >= 6) {
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().GlowTintColor = new Color(1, 0, 0, 1);
				laserBeamGameObject.GetComponent<LightningBoltPrefabScript>().LightningTintColor = new Color(1, 0, 0, 1);
			}

			creditManager.SubtractCredit(upgradeCost);
			upgradeCost = Mathf.RoundToInt(upgradeCost * 6f);

			upgradeButtonCostText (upgrade5Level, maxUpgrade);
			currentUpgradeLevelText.text = upgrade5Level.ToString();
		} else {
			upgradeButtonCostText (upgrade5Level, maxUpgrade);
		}
	}

	void UpgradeEnergyRegeneration () {
		if(upgrade6Level < maxUpgrade){
			upgrade6Level += 1;
			displayManager.DisplayMessage ("Energy Charger: Level "  + upgrade6Level);
			 
			// Faster Regeneration
			//playerGameObject.GetComponent<Player>().regenerateRatePerc = playerGameObject.GetComponent<Player>().regenerateRatePerc * 1.2f;
			playerGameObject.GetComponent<Player>().regenerateTimeGap = playerGameObject.GetComponent<Player>().regenerateTimeGap / 1.5f;

			creditManager.SubtractCredit(upgradeCost);
			upgradeCost = Mathf.RoundToInt(upgradeCost * 3f);

			upgradeButtonCostText (upgrade6Level, maxUpgrade);
			currentUpgradeLevelText.text = upgrade6Level.ToString();
		} else {
			upgradeButtonCostText (upgrade6Level, maxUpgrade);
		}
	}

	void upgradeButtonCostText (int _upgradeLevel, int _maxUpgrade) {
		if (_maxUpgrade != _upgradeLevel) {
			nextUpgradeCostText.text = upgradeCost.ToString();
		} else {
			nextUpgradeCostText.text = "MAX";
			nextUpgradeCostText.alignment = TextAnchor.MiddleCenter;
			nextUpgradeCostText.color = Color.grey;
			moneyImage.SetActive(false);
		}
	}
}


//	public void ConfirmUpgradeStats () {
//		if (creditManager.credit >= upgradeCost && upgradeable){
//			confirmUpgradePanel.Confirm (myConfirmAction1,this.transform,true);
//		} else {
//			confirmUpgradePanel.Confirm (myConfirmAction1,this.transform,false);
//		}
//	}


//void UpgradeStats () {
//
//		if(upgrade1Level < maxUpgrade){
//			upgrade1Level += 1;
//		
//			displayManager.DisplayMessage ("Stats: Level " + upgrade1Level);
//
//			shipGameObject.GetComponent<Ship>().health.MaxVal = shipGameObject.GetComponent<Ship>().health.MaxVal * 1.1f;
//			shipGameObject.GetComponent<Ship>().health.Initialize();
//
//			shipGameObject.GetComponent<Ship>().shield.MaxVal = shipGameObject.GetComponent<Ship>().shield.MaxVal * 1.1f;
//			shipGameObject.GetComponent<Ship>().shield.Initialize();
//
//			playerGameObject.GetComponent<Player>().energy.MaxVal = playerGameObject.GetComponent<Player>().energy.MaxVal * 1.1f;
//			playerGameObject.GetComponent<Player>().energy.Initialize();
//
//			creditManager.SubtractCredit(upgradeCost);
//			upgradeCost = Mathf.RoundToInt(upgradeCost * 1.1f);
//
//			upgradeButtonText ("Stats",upgrade1Level,maxUpgrade);
//		} else {
//			upgradeButtonText ("Stats",upgrade1Level,maxUpgrade);
//		}
//	}