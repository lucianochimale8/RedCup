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
            GameObject obj = Instantiate(particlePrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    #endregion

    #region Getter
    public GameObject GetParticle(Vector3 position)
    {
        if (pool.Count == 0)
            return null;

        GameObject particle = pool.Dequeue();

        particle.transform.position = position;

        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        //ps.Clear();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ps.Play();

        particle.SetActive(true);

        return particle;
    }
    #endregion

    #region Return
    public void ReturnParticle(GameObject particle)
    {
        particle.SetActive(false);
        pool.Enqueue(particle);
    }
    #endregion
}
