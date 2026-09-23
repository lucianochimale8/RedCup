using UnityEngine;

public class WandPickup : MonoBehaviour
{
    public enum PickupState { Idle, PlayerInRange, Collected }

    private PlayerWeaponController playerWeapon;
    private PlayerInteractUI interactUI;
    private PickupState currentState = PickupState.Idle;

    private void Awake()
    {
        interactUI = GetComponentInChildren<PlayerInteractUI>();
    }
    /// <summary>
    /// Cuando entra al rango para agarrar
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (currentState == PickupState.Collected) return;

        if (col.CompareTag("Player"))
        {
            currentState = PickupState.PlayerInRange;
            interactUI?.Show();
            playerWeapon = col.GetComponentInParent<PlayerWeaponController>();
        }     
    }
    /// <summary>
    /// Cuando sale al rango para agarrar
    /// </summary>
    private void OnTriggerExit2D(Collider2D col)
    {
        if (currentState == PickupState.Collected) return;

        if (col.CompareTag("Player"))
        {
            currentState = PickupState.Idle;
            interactUI?.Hide();
            playerWeapon = null;
        }
            
    }
    /// <summary>
    /// Para actualizar para interactuar con E
    /// </summary>
    private void Update()
    {
        if (currentState == PickupState.PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            currentState = PickupState.Collected;
            playerWeapon?.EquipWand();
            Destroy(gameObject);
        }
    }
}

