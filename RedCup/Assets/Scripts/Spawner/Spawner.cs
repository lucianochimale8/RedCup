using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Enemigo")]
    [SerializeField] private GameObject enemyPrefab;
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;
    [Header("Waves")]
    [SerializeField] private int enemiesPrewave, waves;
    [Header("Times")]
    [SerializeField] private float timeBetweenSpawns, timeBetweenWaves;
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
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
                GameManager.Instance.IncreaseEnemiesLeft();
            }

            if (i < waves - 1)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        GameManager.Instance.SetAllWavesSpawned();
    }
}
