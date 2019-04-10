using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public int scoreValue = 10;
    public float timeValue = 10;
    public string collectableColor;
    public AudioClip collectableAudioClip;

    public ParticleSystem deathEffect;

    ScoreManager scoreManager;
    TimeManager timeManager;
    SoundEffectsManager soundEffectsManager;

    private void Awake()
    {
        scoreManager = ScoreManager.Instance();
        timeManager = TimeManager.Instance();
        soundEffectsManager = SoundEffectsManager.Instance();
    }

    private void Start()
    {
        if (gameObject.tag == "Empty Space")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grid Collider")
        {
            Destroy(gameObject);

            /* Object Pooling attempt *************************************
            //gameObject.SetActive(false);
            **************************************************************/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            scoreManager.AddScore(scoreValue);
            timeManager.AddTime(timeValue,collectableColor);

            soundEffectsManager.audioSource.PlayOneShot(collectableAudioClip);

            Destroy(gameObject);

            if (scoreValue <= 0)
            {
                Instantiate(deathEffect.gameObject, transform.position, transform.rotation);
                Destroy(collision.gameObject);
                timeManager.ballDestroyed = true;
            }
        }

        if (collision.gameObject.tag == "Collectable")
        {
            Destroy(gameObject);
        }
    }
}