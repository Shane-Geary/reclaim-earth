using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private int poolSize = 5;
    public int enemiesPerWave = 2;
    public float spawnRate = 2.0f;
    private float spawnXOffset = 2.0f;
    private Vector2 spawnYRange = new Vector2(-3.5f, 3.5f); // Min and max Y values for spawning

    private float testPositionSpawn = 0f;

    private Queue<GameObject> enemyPool;
    private int enemiesSpawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        InitializePool();
        StartCoroutine(SpawnWaves());
    }

    void InitializePool()
    {
        enemyPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            // TODO - Pass data to 
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(spawnRate);
        while (enemiesSpawned < enemiesPerWave)
        {
            Debug.Log($"Spawning enemy at {Time.time}, Spawn Rate: {spawnRate}");
            SpawnEnemy();
            yield return new WaitForSecondsRealtime(spawnRate);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPool.Count > 0)
        {
            //testPositionSpawn = testPositionSpawn + 1;
            //Debug.Log("Spawning enemy: " + enemyPool.Count);
            GameObject enemy = enemyPool.Dequeue();
            float randomY = Random.Range(spawnYRange.x, spawnYRange.y);
            Vector3 spawnPosition = new Vector3(Camera.main.transform.position.x + spawnXOffset, randomY, 0);

            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);

            enemiesSpawned++;
        }
    }

    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
