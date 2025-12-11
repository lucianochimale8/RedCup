using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesPrewave, waves;
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
            }

            if (i < waves - 1)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }
}
