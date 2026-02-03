using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Arma")]
    [SerializeField] private Wand wand;
    [Header("GameObject Prefab")]
    [SerializeField] private GameObject wandPickupPrefab;
    [Header("Punto del drop")]
    [SerializeField] private Transform dropPoint;
    /// <summary>
    /// Referencia para saber si tiene o no el arma
    /// </summary>
    public bool HasWand { get; private set; }
    private void Start()
    {
        wand.gameObject.SetActive(false);
        HasWand = false;
    }
    private void Update()
    {
        if (HasWand && Input.GetKeyDown(KeyCode.G))
        {
            DropWand();
        }
    }
    /// <summary>
    /// Equip
    /// </summary>
    public void EquipWand()
    {
        if (HasWand) return;

        HasWand = true;
        wand.Equip();
    }
    /// <summary>
    /// Drop
    /// </summary>
    public void DropWand()
    { 
        Instantiate(
            wandPickupPrefab,
            dropPoint.position,
            Quaternion.identity
        );
        wand.Unequip();
        HasWand = false;
    }
}
