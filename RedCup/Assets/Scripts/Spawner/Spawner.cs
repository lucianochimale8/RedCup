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
    [Header("Waves")]
    [SerializeField] private int enemiesPrewave, waves;
    [Header("Times")]
    [SerializeField] private float timeBetweenSpawns, timeBetweenWaves;
    [Header("Pool")]
    [SerializeField] private int initialPoolSize = 10;

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
    void Start()
    {
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        // este bucle for exterior hace esa accion de instanciar 3 enemigos se instancie por oleadas
        for (int i = 0; i < waves; i++)
        {
            // este bucle for interno, instancia 3 enemios
            for (int j = 0; j < enemiesPrewave; j++)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);

                int index = j % spawnPoints.Length;

                // agregar enemy pool
                GameObject enemy = GetEnemyFromPool();
                enemy.transform.position = spawnPoints[index].position;
                enemy.transform.SetParent(enemiesParent);
                enemy.SetActive(true);

                enemy.GetComponent<EnemyHealth>().SetSpawner(this);

                //GameManager.Instance.IncreaseEnemiesLeft();
                LevelObjectiveManager.Instance.RegisterEnemySpawn();
            }

            if (i < waves - 1)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        //GameManager.Instance.SetAllWavesSpawned();
        LevelObjectiveManager.Instance.SetAllWavesSpawned();
    }

    private GameObject GetEnemyFromPool()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            return enemy;
        }
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}
