using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    
    public float maxSpeed = 10f;
    public AudioClip bouncAudioClip;

    SoundEffectsManager soundEffectsManager;

    private static Ball ball;

    public static Ball Instance()
    {
        if (!ball)
        {
            ball = FindObjectOfType(typeof(Ball)) as Ball;
            if (!ball)
            {
                Debug.LogError("There needs to be one active LimitSpeed script on a GameObject in your scene.");
            }
        }
        return ball;
    }

    private void Awake()
    {
        soundEffectsManager = SoundEffectsManager.Instance();
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Line Collider" && Time.timeScale >= 1f)
        {
            
            soundEffectsManager.audioSource.PlayOneShot(bouncAudioClip);

        }
    }
}
