using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public float startWaveTime = 1f;
	public Wave[] waves;
	private EnemyCountManager enemyCountManager;
    private Enemy spawnedEnemy;
    private Wave currentWave;
    private int currentWaveNumber;
    private int wavesRemaining;
    private int enemiesRemainingToSpawn;
    private int enemiesRemainingAlive;
	[HideInInspector]
	public int sumEnemies;
    private float nextSpawnTime;
    private float nextWaveTime;

    private void Awake () {
		enemyCountManager = EnemyCountManager.Instance();
		SumEnemies(waves);
		//Debug.Log("No. Enemies In Spawner**************************************** " + sumEnemies.ToString());
	}

    private void Start () {
		NextWave(); // Wave 1
		wavesRemaining = waves.Length;
		nextWaveTime = Time.time + currentWave.timeToNextWave + (currentWave.timeBetweenSpawns * currentWave.enemyCount) + startWaveTime;
        startWaveTime = Time.time + startWaveTime;
	}

    private void Update() {
		if(Time.time > startWaveTime){
			if(enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
				enemiesRemainingToSpawn --;
				nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
				spawnedEnemy = Instantiate(currentWave.enemy, transform.position, transform.rotation) as Enemy;
				spawnedEnemy.OnDeath += OnEnemyDeath; // the event OnDeath from Living Entity notifies us here and adds the subscribers (new enemies being spawned) to the published event
			}

			if(wavesRemaining > 0 && Time.time > nextWaveTime){
				wavesRemaining--;
                NextWave();
                nextWaveTime = Time.time + currentWave.timeToNextWave + (currentWave.timeBetweenSpawns * currentWave.enemyCount);
			}

            if(wavesRemaining == 0 && enemiesRemainingToSpawn == 0){
                Destroy(gameObject);
            }
		}
	}

    private void OnEnemyDeath(){ // subscriber (same signature as OnDeath in Living Entity)
		enemiesRemainingAlive --;

        /*
			This is subscribed to the OnDeath event
		 	so what ever needs to happen when an
		 	enemy is killed (dies by health = 0) can be put here. Except Scoring and Credit is dealt with in Enemy script 				
		*/

		enemyCountManager.AddDeadEnemies();
        //Debug.Log("Enemy Dies in EnemySpawner");
	}

    private void NextWave () {
		currentWaveNumber ++;
		// print ("Wave " + currentWaveNumber);
		if (currentWaveNumber - 1 < waves.Length) {
			currentWave = waves[currentWaveNumber - 1];

			enemiesRemainingToSpawn = currentWave.enemyCount;
			enemiesRemainingAlive = enemiesRemainingToSpawn;
		}
	}	

	public int SumEnemies(Wave[] _enemyWaveArray){
		
		foreach (Wave enemy in _enemyWaveArray) {
			sumEnemies += enemy.enemyCount;
		}

		return sumEnemies;
	}

	[System.Serializable]
	public class Wave {
		public int enemyCount;
		public float timeBetweenSpawns;
		public Enemy enemy; 
		public float timeToNextWave;
	}
}
