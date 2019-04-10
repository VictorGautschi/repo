using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class PickupManager : MonoBehaviour {

	/*	
		Pickups are added on the pickup scripts (EnergyCapsule & Medkit)
		Pickups are used when clicking on the respective buttons
		The ConfirmPanel will only appear when the pickup can be used
	*/

	[SerializeField]
	private float healthAmount;
	[SerializeField]
	private float useMedkitEnergyCost;
	[SerializeField]
	private float useEnergyCapsuleReplenish;
	[SerializeField]
	private float useLightningBlastEnergyCost;

	private Player player;
	private Ship ship;
	private LightningWeapon lightningWeapon;
	private Color originalLightningBlastEnergyCostColor;
	private Color originalMedkitEnergyCostColor;

	public Button useMedkitButton;
	public Button useEnergyCapsuleButton;
	public Button useLightningBlastButton;
	public Text displayMedkits;
	public Text displayEnergyCapsules;
	public Text displayLightningBlasts;
	public Text displayMedkitEnergyCost;
	public Text displayUseEnergyCapsuleReplenish;
	public Text displayUseMedkitReplenish;
	public Text displayLightningBlastEnergyCost;

	private int numberOfMedkits;
	private int numberOfEnergyCapsules;
	private int numberOfLightningBlasts;

	private float startTimeOfPressingButton;

	public ConfirmPanel confirmPickupUsePanel;

	private UnityAction myConfirmAction1;
	private UnityAction myConfirmAction2;
	private UnityAction myConfirmAction3;

    private SoundEffectsManager soundEffectsManager;
    public AudioClip useEnergyCapsuleAudio;
    public AudioClip useMedkitAudio;
    public AudioClip useLightningBlastAudio;
    public AudioClip collectPickupAudio;

	private static PickupManager pickupManager;

	public static PickupManager Instance () {
		if (!pickupManager) {
			pickupManager = FindObjectOfType(typeof (PickupManager)) as PickupManager;
			if (!pickupManager){
				Debug.LogError ("There needs to be one active PickupManager script on a GameObject in your scene.");
			}
		}
		return pickupManager;
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();

		myConfirmAction1 = new UnityAction (UseMedkit);
		myConfirmAction2 = new UnityAction (UseEnergyCapsule);
		myConfirmAction3 = new UnityAction (UseLightningBlast);

		displayMedkitEnergyCost.text = (useMedkitEnergyCost * 100).ToString();
		displayUseEnergyCapsuleReplenish.text = (useEnergyCapsuleReplenish * 100).ToString();
		displayUseMedkitReplenish.text = (healthAmount * 100).ToString();
		displayLightningBlastEnergyCost.text = (useLightningBlastEnergyCost * 100).ToString();

		originalMedkitEnergyCostColor = displayMedkitEnergyCost.color;
		originalLightningBlastEnergyCostColor = displayLightningBlastEnergyCost.color;
	}

    private void Start()
    {
        soundEffectsManager = SoundEffectsManager.Instance();

        displayMedkits.color = Color.red;
        displayMedkits.text = numberOfMedkits.ToString();

        displayEnergyCapsules.color = Color.red;
        displayEnergyCapsules.text = numberOfEnergyCapsules.ToString();

        displayLightningBlasts.color = Color.red;
        displayLightningBlasts.text = numberOfLightningBlasts.ToString();
    }

    void Update () {
		if (player.energy.value < useLightningBlastEnergyCost) {
			displayLightningBlastEnergyCost.color = Color.red;
		} else {
			displayLightningBlastEnergyCost.color = originalLightningBlastEnergyCostColor;
		}

		if (player.energy.value < useMedkitEnergyCost) {
			displayMedkitEnergyCost.color = Color.red;
		} else {
			displayMedkitEnergyCost.color = originalMedkitEnergyCostColor;
		}

		if (Input.GetMouseButtonDown(0)) {
			startTimeOfPressingButton = Time.time;
		}		
	}

	public void ConfirmUseMedkit () {
		if (numberOfMedkits > 0 && ship.health.value < ship.fullHealth  && (Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			confirmPickupUsePanel.Confirm (myConfirmAction1,useMedkitButton.transform);
		} 
	}

	public void ConfirmUseEnergyCapsule () {
		if (numberOfEnergyCapsules > 0 && player.energy.value < player.fullEnergy  && (Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			confirmPickupUsePanel.Confirm (myConfirmAction2,useEnergyCapsuleButton.transform);
		} 
	}

	public void ConfirmUseLightningBlast () {
		if(numberOfLightningBlasts > 0 && player.energy.value >= useLightningBlastEnergyCost  && (Time.time - startTimeOfPressingButton) < StaticVariables.infoWindowFadeDelay){
			confirmPickupUsePanel.Confirm (myConfirmAction3,useLightningBlastButton.transform);	
		}	
	}

	void UseMedkit () {
		if (ship.health.value < ship.fullHealth && numberOfMedkits > 0 && player.energy.value >= useMedkitEnergyCost) {
			numberOfMedkits--;
			ship.health.value += healthAmount; // This need to be capped at the full health
			player.energy.value -= useMedkitEnergyCost; // This need to be bottomed at 0
			if (ship.health.value > ship.fullHealth){
				ship.health.value = ship.fullHealth;
			}
            soundEffectsManager.audioSource.PlayOneShot(useMedkitAudio, soundEffectsManager.audioSource.volume);
		}

        if (numberOfMedkits > 0)
        {
            displayMedkits.color = Color.green;
        }
        else
        {
            displayMedkits.color = Color.red;
        }

		displayMedkits.text = numberOfMedkits.ToString();
	}

	void UseEnergyCapsule () {
		if (player.energy.value < player.fullEnergy && numberOfEnergyCapsules > 0) {
			numberOfEnergyCapsules--;
			player.energy.value += useEnergyCapsuleReplenish; // This need to be capped at the full energy 
			if (player.energy.value > player.fullEnergy) {
				player.energy.value = player.fullEnergy;
			}
            soundEffectsManager.audioSource.PlayOneShot(useEnergyCapsuleAudio, soundEffectsManager.audioSource.volume);
		}

        if (numberOfEnergyCapsules > 0)
        {
            displayEnergyCapsules.color = Color.green;
        }
        else
        {
            displayEnergyCapsules.color = Color.red;
        }

		displayEnergyCapsules.text = numberOfEnergyCapsules.ToString();
	}

	void UseLightningBlast() {
		if (numberOfLightningBlasts > 0 && player.energy.value >= useLightningBlastEnergyCost) {
			numberOfLightningBlasts--;
			lightningWeapon = GameObject.FindGameObjectWithTag("Lightning Blast").GetComponent<LightningWeapon>();
			lightningWeapon.LightningBlast();
            soundEffectsManager.audioSource.PlayOneShot(useLightningBlastAudio, soundEffectsManager.audioSource.volume);
			player.energy.value -= useLightningBlastEnergyCost;
		}

        if (numberOfLightningBlasts > 0)
        {
            displayLightningBlasts.color = Color.green;
        }
        else
        {
            displayLightningBlasts.color = Color.red;
        }

		displayLightningBlasts.text = numberOfLightningBlasts.ToString();
	}

	public void CollectMedkit () {
		numberOfMedkits++;
        soundEffectsManager.audioSource.PlayOneShot(collectPickupAudio, soundEffectsManager.audioSource.volume);
        displayMedkits.color = Color.green;
        displayMedkits.text = numberOfMedkits.ToString();
	}

	public void CollectEnergyCapsule () {
		numberOfEnergyCapsules++;
        soundEffectsManager.audioSource.PlayOneShot(collectPickupAudio, soundEffectsManager.audioSource.volume);
        displayEnergyCapsules.color = Color.green;
		displayEnergyCapsules.text = numberOfEnergyCapsules.ToString();
	}

	public void CollectLightningBlast () {
		numberOfLightningBlasts++;
        soundEffectsManager.audioSource.PlayOneShot(collectPickupAudio, soundEffectsManager.audioSource.volume);
        displayLightningBlasts.color = Color.green;
		displayLightningBlasts.text = numberOfLightningBlasts.ToString();
	}
}