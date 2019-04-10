using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables : MonoBehaviour {

	public static float xOffset = 1f; // big spaces
	public static float yOffset = 0.8662f; // big spaces - coefficient from y to x
//	public static float xOffset = 0.882f; // with spaces
//	public static float yOffset = 0.764f; // with spaces
//	public static float xOffset = 0.867f;  // without spaces
//	public static float yOffset = 0.7505f; // without spaces

    public static int gridWidth  = 23;
    public static int gridHeight = 23;

	// If the upgrade and other buttons are pressed for longer than the below, then the info window starts to fade in (used in PickupManager and UpgradeManager)
	public static float infoWindowFadeDelay = 0.2f;

}
