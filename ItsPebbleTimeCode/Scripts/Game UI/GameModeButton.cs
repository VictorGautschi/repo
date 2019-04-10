using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeButton : MonoBehaviour {

    ScoreManager scoreManager;

    void Awake () {
        scoreManager = ScoreManager.Instance();
	}
	
    public void PressButton()
    {
        scoreManager.ResetScore();
    }
}
