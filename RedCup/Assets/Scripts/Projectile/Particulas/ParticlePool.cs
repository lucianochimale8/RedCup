using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;

    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    #region Unity Lifecycle
    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(particlePrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    #endregion

    #region Get
    public GameObject GetParticle(Vector3 position)
    {
        GameObject particle;

        if (pool.Count > 0)
        {
            particle = pool.Dequeue();
        }
        else
        {
            particle = Instantiate(particlePrefab);
        }

        
        particle.SetActive(true);

        return particle;
    }
    #endregion

    #region Return
    public void ReturnParticle(GameObject particle)
    {
        particle.SetActive(true);
        pool.Enqueue(particle);
    }
    #endregion
}
