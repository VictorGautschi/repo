using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{

    // This script goes on the object in the scene that will persist. 
    // singleton design pattern (one object per scene)
    public static GameControl control;

    [HideInInspector]
    public bool adsDisabled = false;
    [HideInInspector]
    public bool therapyModePurchased = false; // not being used right now
    [HideInInspector]
    public bool purchasesRestoredChecked = false;
    [HideInInspector]
    public string gamerName = "";
    [HideInInspector]
    public bool firstTime = true;
    [HideInInspector]
    public bool musicOff = false;
    [HideInInspector]
    public int bestScore = 0;
    [HideInInspector]
    public string savedPrice;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(this.gameObject);
            control = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Write to a file
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfoBBB.dat"); // filename and where it is going (FileMode.Open is Unity controlled)    
        PlayerData data = new PlayerData(); // new instance of PlayerData

        data.adsDisabled = adsDisabled;
        data.therapyModePurchased = therapyModePurchased;
        data.purchasesRestoredChecked = purchasesRestoredChecked;
        data.gamerName = gamerName;
        data.firstTime = firstTime;
        data.musicOff = musicOff;
        data.bestScore = bestScore;
        data.savedPrice = savedPrice;

        bf.Serialize(file, data); // take our data and write it to file as per variables above
        file.Close();
    }

    // Read from a file
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfoBBB.dat"))
        { // to avoid error
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfoBBB.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            adsDisabled = data.adsDisabled;
            therapyModePurchased = data.therapyModePurchased;
            purchasesRestoredChecked = data.purchasesRestoredChecked;
            gamerName = data.gamerName;
            firstTime = data.firstTime;
            musicOff = data.musicOff;
            bestScore = data.bestScore;
            savedPrice = data.savedPrice;
        }
    }
}

[Serializable] // tells Unity that this can be saved to a file
class PlayerData
{ // what data to write to the file
    public bool adsDisabled = false;
    public bool therapyModePurchased = false;
    public bool purchasesRestoredChecked = false;
    public string gamerName;
    public bool firstTime;
    public bool musicOff;
    public int bestScore;
    public string savedPrice;
}
