using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Arma")]
    [SerializeField] private Wand wand;
    [Header("GameObject Prefab")]
    [SerializeField] private GameObject wandPickupPrefab;
    [Header("Punto del drop")]
    [SerializeField] private Transform dropPoint;
    private void Start()
    {
        wand.gameObject.SetActive(false);
        // Si el GameManager dice que tiene arma, la equipa
        if (GameManager.Instance.HasWand)
        {
            EquipVisual();
        }
    }
    private void Update()
    {
        if (GameManager.Instance.HasWand && Input.GetKeyDown(KeyCode.G))
        {
            DropWand();
        }
    }
    /// <summary>
    /// Equip
    /// </summary>
    // Metodo para equipar el arma y avisar que fue recogida
    public void EquipWand()
    {
        if (GameManager.Instance.HasWand) return;

        GameManager.Instance.SetWand(true);
        EquipVisual();
    }
    // Metodo para lo visual
    private void EquipVisual()
    {
        wand.Equip();
    }
    /// <summary>
    /// Drop
    /// </summary>
    public void DropWand()
    {
        // Si no tiene un arma el jugador, retornar
        if (!GameManager.Instance.HasWand) return;

        Instantiate(
            wandPickupPrefab,
            dropPoint.position,
            Quaternion.identity
        );

        wand.Unequip();
        GameManager.Instance.SetWand(false);
    }
}
