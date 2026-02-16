using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private Transform projectilesParent;

    private List<GameObject> pool;
    private void Awake()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject p = Instantiate(projectilePrefab, projectilesParent);
            p.SetActive(false);
            pool.Add(p);
        }
    }
    public GameObject GetProjectile()
    {
        // Buscar un proyectil inactivo
        foreach (var p in pool)
        {
            if (!p.activeInHierarchy)
            {
                //Debug.Log("PROYECTIL DADO DESDE EL POOL");
                return p;
            }
        }
        // Si todos están activos, no crear más, solo devolver null
        Debug.Log("NO HAY PROYECTILES DISPONIBLES EN EL POOL");
        return null;
    }
}
