using UnityEngine;

public class Wand : MonoBehaviour
{
    [Header("Disparo")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private ProjectilePool pool;
    [Header("Audio")]
    [SerializeField] private AudioClip spell;

    public void Shoot()
    {
        Debug.Log("WAND SHOOT LLAMADO");

        GameObject p = pool.GetProjectile();
        if (p != null)
        {
            p.transform.position = firePoint.position;
            p.transform.rotation = firePoint.rotation;
            p.SetActive(true);

            p.GetComponent<Projectile>().Initialize(firePoint.right);
            //AudioManager.Instance.PlaySoundEffect(spell, 0.5f);
        }
        else
        {
            Debug.Log("No hay proyectiles disponibles");
        }
    }
}
