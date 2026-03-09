using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemigo")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemiesParent;
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;
    [Header("Wave Settings")]
    [SerializeField] private int enemiesPrewave, waves;
    [SerializeField] private float timeBetweenSpawns, timeBetweenWaves;
    [Header("Pool")]
    [SerializeField] private int initialPoolSize = 10;

    public int TotalEnemiesToSpawn => waves * enemiesPrewave;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private bool isStopped;

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
        // este bucle for exterior hace esa accion de instanciar 3 enemigos se instancie por oleadas
        for (int i = 0; i < waves; i++)
        {
            // este bucle for interno, instancia 3 enemigos
            for (int j = 0; j < spawnPoints.Length; j++)
            {
                while (isStopped)
                    yield return null;
                Transform spawnPoint = spawnPoints[j];

                GameObject enemy = GetEnemyFromPool();
                enemy.transform.position = spawnPoint.position;
                enemy.transform.SetParent(enemiesParent);

                enemy.GetComponent<EnemyHealth>().SetSpawner(this);
                enemy.SetActive(true);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            if (i < waves - 1)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }

    private GameObject GetEnemyFromPool()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        GameObject enemy = Instantiate(enemyPrefab, enemiesParent);
        return enemy;
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
    private void StopSpawner()
    {
        isStopped = true;
    }

    private void ResumeSpawner()
    {
        isStopped = false;
    }
}
