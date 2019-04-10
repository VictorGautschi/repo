using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	// This script goes on the object in the scene that will persist. 
 	// singleton design pattern (one object per scene)
	public static GameControl control;

	[HideInInspector]
	public int levelReached;
	[HideInInspector]
	public int[] numberStarsPerLevel = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}; //32 levels, may need to increase
    [HideInInspector]
    public bool adsDisabled = false;
    [HideInInspector]
    public bool purchasesRestoredChecked = false;
    [HideInInspector]
    public string savedPrice;

	void Awake () {
		if (control == null) {
			DontDestroyOnLoad(this.gameObject);
			control = this;
		} else if (control != null) {
			Destroy(gameObject);
		}
	}

	// Write to a file
	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); // filename and where it is going (FileMode.Open is Unity controlled)	
		PlayerData data = new PlayerData(); // new instance of PlayerData

		data.levelReached = levelReached;
		data.numberStarsPerLevel = numberStarsPerLevel;
        data.adsDisabled = adsDisabled;
        data.purchasesRestoredChecked = purchasesRestoredChecked;
        data.savedPrice = savedPrice;

		bf.Serialize(file, data); // take our data and write it to file as per variables above
		file.Close();
	}

	// Read from a file
	public void Load() {
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat")) { // to avoid error
			BinaryFormatter bf = new BinaryFormatter(); 
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

            levelReached = data.levelReached;
			numberStarsPerLevel = data.numberStarsPerLevel;
            adsDisabled = data.adsDisabled;
            purchasesRestoredChecked = data.purchasesRestoredChecked;
            savedPrice = data.savedPrice;
		}
	}
}

[Serializable] // tells Unity that this can be saved to a file
class PlayerData { // what data to write to the file
	public int levelReached;
	public int[] numberStarsPerLevel = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}; //32 levels, may need to increase
    public bool adsDisabled = false;
    public bool purchasesRestoredChecked = false;
    public string savedPrice;
}
