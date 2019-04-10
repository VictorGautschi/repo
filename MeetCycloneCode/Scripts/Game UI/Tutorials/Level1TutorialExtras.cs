using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1TutorialExtras : MonoBehaviour {

    bool tutHelpBool;
    bool upgradeable;
    bool cycloneActive;
    bool shipHealthAndShieldActive;
    bool upgradeAndCreditActive;

    float cycloneLabelTime;
    float shipHealthAndShieldLabelTime;

    public GameObject upgradeSpeechBubble;
    public GameObject creditSpeechBubble;
    public GameObject healthSpeechBubble;
    public GameObject shieldSpeechBubble;
    public GameObject cycloneLabel;
    public GameObject shipLabel;

    public Text nextUpgradeCostText;

    public int upgradeCost = 30; // hard coded for now
    private CreditManager creditManager;

    private void Start()
    {
        tutHelpBool = true; // To help make the tutorial speech bubble for upgrades come up only once in the level
        creditManager = CreditManager.Instance();
        cycloneLabelTime = Time.time + 12f;
        shipHealthAndShieldLabelTime = cycloneLabelTime + 8f;
        tutHelpBool = true; // To help make the tutorial speech bubble for upgrades come up only once in the level
        cycloneActive = true;
        upgradeAndCreditActive = true;
        shipHealthAndShieldActive = true;
    }

    void Update()
    {
        if (nextUpgradeCostText.color == Color.grey)
        {
            upgradeable = false;
        }
        else
        {
            upgradeable = true;
        }

        if (upgradeable)
        {
            if(creditManager.credit >= upgradeCost && nextUpgradeCostText.color != Color.red){
                if (tutHelpBool && SceneManager.GetActiveScene().name == "02 Level_01")
                {
                    tutHelpBool = false;

                    upgradeSpeechBubble.SetActive(true);
                    creditSpeechBubble.SetActive(true);

                } 

                if(creditManager.credit >= upgradeCost * 3 && upgradeAndCreditActive){
                    upgradeSpeechBubble.SetActive(false);
                    creditSpeechBubble.SetActive(false);
                    upgradeAndCreditActive = false;
                }
            } else {
                if (!tutHelpBool)
                {
                    upgradeSpeechBubble.SetActive(false);
                    creditSpeechBubble.SetActive(false);
                }
            }
        }

        if (Time.time > cycloneLabelTime && cycloneActive) {
            cycloneLabel.SetActive(false);
            healthSpeechBubble.SetActive(true);
            shieldSpeechBubble.SetActive(true);
            shipLabel.SetActive(true);
            cycloneActive = false;
        }

        if (Time.time > shipHealthAndShieldLabelTime && shipHealthAndShieldActive)
        {
            healthSpeechBubble.SetActive(false);
            shieldSpeechBubble.SetActive(false);
            shipLabel.SetActive(false);
            shipHealthAndShieldActive = false;
        }
    }
}
