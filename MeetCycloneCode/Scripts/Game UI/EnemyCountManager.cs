using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCountManager : MonoBehaviour
{
    private float timer;
    private float timer2;
    private float timer3;

    int numberEnemiesDead;
    int numberEnemiesKilled;
    [HideInInspector]
    GameObject[] enemySpawners;
    Ship ship;

    private ModalManager modalManager;
    private LevelManager levelManager;
    private MusicManager musicManager;
    private SoundEffectsManager soundEffectsManager;

    public AudioClip winLevelAudio;

    [HideInInspector]
    public int sumEnemies;

    private static EnemyCountManager enemyCountManager;

    public static EnemyCountManager Instance()
    {
        if (!enemyCountManager)
        {
            enemyCountManager = FindObjectOfType(typeof(EnemyCountManager)) as EnemyCountManager;
            if (!enemyCountManager)
            {
                Debug.LogError("There needs to be one active EnemyCountManager script on a GameObject in your scene.");
            }
        }
        return enemyCountManager;
    }

    void Awake()
    {
        modalManager = ModalManager.Instance();
        levelManager = LevelManager.Instance();
        ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();

        numberEnemiesDead = 0;
        enemySpawners = GameObject.FindGameObjectsWithTag("Enemy Spawner");
    }

    void Start()
    {
        musicManager = MusicManager.Instance();
        soundEffectsManager = SoundEffectsManager.Instance();
        SumEnemies(enemySpawners);
        //Debug.Log("No. Enemies Total**************************************** " + sumEnemies.ToString());
    }

    void Update()
    {
        if (numberEnemiesDead >= sumEnemies && ship.health.value > 0)
        {
            timer2 += Time.deltaTime;
            if (timer2 <= 0.4f) // So that originalVolume2 captured before the fade starts
            {
                timer2 = 0.5f;
                musicManager.CaptureVolume();
                //Debug.Log("Music Volume -----------------------------------------------------------------" + musicManager.originalVolume2);
            }

            timer3 += Time.deltaTime;
            if(timer3 > 0.5f){
                musicManager.FadeMusicOut(5f); // this is being run every frame after enemies all die. not good. 
                DestroyEnemiesLeft(); // safety catch all left behind enemies
            }

            timer += Time.deltaTime;
            if(timer > 4f)
            {
                timer = 0f;
                //Debug.Log("You Win**************************************** " + (numberEnemiesKilled).ToString());
                soundEffectsManager.audioSource.PlayOneShot(winLevelAudio, soundEffectsManager.audioSource.volume);
                levelManager.WinLevel();
                modalManager.RestartQuitNext();
                Destroy(gameObject); // This is why it is on its own GameObject
            }
        }
    }

    public void AddDeadEnemies()
    {
        numberEnemiesDead++;
    }

    int SumEnemies(GameObject[] _enemySpawnerArray)
    {
        foreach (GameObject enemySpawner in _enemySpawnerArray)
        {
            sumEnemies += enemySpawner.GetComponent<EnemySpawner>().sumEnemies;
            //Debug.Log("No. Enemies Per Spawner**************************************** " + sumEnemies.ToString());
        }
        return sumEnemies;
    }

    // This is to kill all enemies that were not spawned through the spawner when level is done
    void DestroyEnemiesLeft()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameobject in Enemies)
        {
            Enemy enemy = gameobject.GetComponent<Enemy>();
            Instantiate(enemy.deathEffect);
            Object.Destroy(gameobject);
        }
    }
}
