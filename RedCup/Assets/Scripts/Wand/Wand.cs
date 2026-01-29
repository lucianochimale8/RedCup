using UnityEngine;

public class Wand : MonoBehaviour
{
    [Header("Disparo")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private ProjectilePool pool;
    [Header("Audio")]
    [SerializeField] private AudioClip spell;
    /// <summary>
    /// Identificar si el arma esta equipada
    /// </summary>
    public bool IsEquipped { get; private set; }

    #region Equip and Unequip
    public void Equip()
    {
        IsEquipped = true;
        gameObject.SetActive(true);
    }

    public void Unequip()
    {
        IsEquipped = false;
        gameObject.SetActive(false);
    }
    #endregion
    #region Disparo
    public void Shoot()
    {
        Debug.Log("WAND SHOOT LLAMADO");
        // Si no hay arma equipada, no poder disparar
        if (!IsEquipped) return;

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
    #endregion
}
