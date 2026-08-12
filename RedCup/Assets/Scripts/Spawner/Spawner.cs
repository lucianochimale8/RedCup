using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnerState
    {
        Idle,
        Spawning,
        Paused,
        Completed
    }

    [Header("Enemigo")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemiesParent;
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;
    [Header("Wave Settings")]
    [SerializeField] private int enemiesPrewave = 3;
    [SerializeField] private int waves = 3;
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [Header("Pool")]
    [SerializeField] private int initialPoolSize = 10;

    public int TotalEnemiesToSpawn => waves * enemiesPrewave;

    private SpawnerState currentState = SpawnerState.Idle;
    public SpawnerState CurrentState => currentState;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemiesParent);
            enemy.SetActive(false);
            pool.Enqueue(enemy);
        }
    }
    private void OnEnable()
    {
        GameEvents.OnLevelStopped += StopSpawner;
        GameEvents.OnLevelResumed += ResumeSpawner;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelStopped -= StopSpawner;
        GameEvents.OnLevelResumed -= ResumeSpawner;
    }
    private void Start()
    {
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        currentState = SpawnerState.Spawning;

        // este bucle for exterior hace esa accion de instanciar 3 enemigos se instancie por oleadas
        for (int i = 0; i < waves; i++)
        {
            // este bucle for interno, instancia 3 enemigos
            for (int j = 0; j < spawnPoints.Length; j++)
            {
                while (currentState == SpawnerState.Paused)
                {
                    yield return null;
                }

                Transform spawnPoint = spawnPoints[j];

                GameObject enemy = GetEnemyFromPool();
                enemy.transform.position = spawnPoint.position;
                enemy.transform.SetParent(enemiesParent);

                EnemyHealth healthComponent = enemy.GetComponent<EnemyHealth>();
                if (healthComponent != null)
                {
                    healthComponent.SetSpawner(this);
                }

                enemy.SetActive(true);

                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            if (i < waves - 1)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        currentState = SpawnerState.Completed;
    }

    private GameObject GetEnemyFromPool()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }

        return Instantiate(enemyPrefab, enemiesParent);
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
    private void StopSpawner()
    {
        if (currentState == SpawnerState.Spawning)
        {
            currentState = SpawnerState.Paused;
        }
    }

    private void ResumeSpawner()
    {
        if (currentState == SpawnerState.Paused)
        {
            currentState = SpawnerState.Spawning;
        }
    }
}
