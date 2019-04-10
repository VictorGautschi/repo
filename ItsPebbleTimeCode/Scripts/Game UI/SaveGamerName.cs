using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGamerName : MonoBehaviour {

    public InputField inputField;
    public GameObject collectNamePanel;
    public HSController hSController;

    private void Awake()
    {
        GameControl.control.Load();

        if (GameControl.control.gamerName != "")
        {
            collectNamePanel.gameObject.SetActive(false);
        }
    }

    public void SaveName()
    {
        GameControl.control.gamerName = inputField.text;

        GameControl.control.Save();

        collectNamePanel.gameObject.SetActive(false);

        hSController.StartRunGetAndPostScoresLoop();
    }
}
