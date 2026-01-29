using UnityEngine;

public class WandPickup : MonoBehaviour
{
    /// <summary>
    /// Bandera para saber si agarro el arma
    /// </summary>
    private bool canPick;
    /// <summary>
    /// Cuando entra al rango para agarrar
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Se puede agarrar");
        if (col.CompareTag("Player"))
            canPick = true;
    }
    /// <summary>
    /// Cuando sale al rango para agarrar
    /// </summary>
    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("NO se puede agarrar");
        if (col.CompareTag("Player"))
            canPick = false;
    }
    /// <summary>
    /// Para actualizar para interactuar con E
    /// </summary>
    private void Update()
    {
        if (canPick && Input.GetKeyDown(KeyCode.E))
        {
            FindAnyObjectByType<PlayerWeaponController>().EquipWand();
            Destroy(gameObject);
        }
    }
}
