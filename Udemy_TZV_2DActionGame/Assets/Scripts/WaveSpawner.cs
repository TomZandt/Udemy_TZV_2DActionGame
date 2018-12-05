using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //****************************************************************************************************
    //****************************************************************************************************
    [System.Serializable]
    public class Wave
    {
        [Header("The enemies to spawn")]
        public Enemy[] enemies;

        [Header("The number of enemies to spawn")]
        public int count = 1;

        [Header("The time between enemy spawns in seconds")]
        public float timeBetweenSpawns = 10f;
    }
    //****************************************************************************************************
    //****************************************************************************************************

    [Header("The number of waves")]
    public Wave[] waves;

    [Header("The spawn points")]
    public Transform[] spawnPoints;

    [Header("The time between each wave in seconds")]
    public float timeBetweenWaves = 30f;

    [Header("The boss prefab")]
    public GameObject boss;

    [Header("The spawn point for the boss")]
    public Transform bossSpawnPoint;

    private Wave currentWave;
    private int currentWaveIndex = 0;
    private Transform playerTransform;
    private bool finishedSpawning = false;

    //****************************************************************************************************
    private void Start()
    {
        // Assign the players transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Start next wave
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    //****************************************************************************************************
    IEnumerator StartNextWave(int index)
    {
        // Wait for the wave timer
        yield return new WaitForSeconds(timeBetweenWaves);

        StartCoroutine(SpawnWave(index));
    }

    //****************************************************************************************************
    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for (int i = 0; i < currentWave.count; i++)
        {
            // If the player is dead
            if (playerTransform == null)
            {
                // Exit
                yield break;
            }

            // Choose a random enemy from the current wave
            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];

            // Choose a random spawn point
            Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawn our enemies
            Instantiate(randomEnemy, randomSpawn.position, randomSpawn.rotation);

            // If we have spawned all the enemies in the wave
            if (i == currentWave.count - 1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }

            // Wait for summon time
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    //****************************************************************************************************
    private void Update()
    {
        // If we have finshed spawning all the enemis in the wave and they are all dead
        if (finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;

            // If there is another wave
            if (currentWaveIndex + 1 < waves.Length)
            {
                // Start next wave
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                // Spawn in boss
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
            }
        }
    }
}
