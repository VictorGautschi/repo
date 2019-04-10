using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{

    [HideInInspector]
    public AudioSource audioSource;

    public static SoundEffectsManager soundEffectsManager;

    public static SoundEffectsManager Instance()
    {
        if (!soundEffectsManager)
        {
            soundEffectsManager = FindObjectOfType(typeof(SoundEffectsManager)) as SoundEffectsManager;
            if (!soundEffectsManager)
            {
                Debug.LogError("There needs to be one active SoundEffectsManager script on a GameObject in your scene.");
            }
        }
        return soundEffectsManager;
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Sound Effects Manager");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
        //Debug.Log("Don't destroy on load: " + name);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
