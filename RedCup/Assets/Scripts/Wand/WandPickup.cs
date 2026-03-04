using UnityEngine;

public class WandPickup : MonoBehaviour
{
    /// <summary>
    /// Referencia al weapon controller del jugador
    /// </summary>
    private PlayerWeaponController playerWeapon;
    /// <summary>
    /// Referencia al Player Interact UI
    /// </summary>
    private PlayerInteractUI interactUI;
    /// <summary>
    /// Bandera para saber si agarro el arma
    /// </summary>
    private bool canPick;

    private void Awake()
    {
        interactUI = GetComponentInChildren<PlayerInteractUI>();
    }
    /// <summary>
    /// Cuando entra al rango para agarrar
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Se puede agarrar");
        
        if (col.CompareTag("Player"))
        {
            canPick = true;
            interactUI?.Show();
            playerWeapon = col.GetComponentInParent<PlayerWeaponController>();
        }     
    }
    /// <summary>
    /// Cuando sale al rango para agarrar
    /// </summary>
    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("NO se puede agarrar");
        if (col.CompareTag("Player"))
        {
            canPick = false;
            interactUI?.Hide();
            playerWeapon = null;
        }
            
    }
    /// <summary>
    /// Para actualizar para interactuar con E
    /// </summary>
    private void Update()
    {
        if (canPick && Input.GetKeyDown(KeyCode.E))
        {
            playerWeapon?.EquipWand();
            Destroy(gameObject);
        }
    }
}

