using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Arma")]
    [SerializeField] private Wand wand;
    [Header("GameObject Prefab")]
    [SerializeField] private GameObject wandPickupPrefab;
    [Header("Punto del drop")]
    [SerializeField] private Transform dropPoint;

    #region Unity Lifecycle
    private void OnEnable()
    {
        GameEvents.OnWandStateChanged += HandleWandChanged;
        GameEvents.OnPlayerDied += HandlePlayerDied;
    }
    private void OnDisable()
    {
        GameEvents.OnWandStateChanged -= HandleWandChanged;
        GameEvents.OnPlayerDied -= HandlePlayerDied;
    }
    private void Start()
    {
        wand.gameObject.SetActive(false);

        if (GameManager.Instance != null)
        {
            HandleWandChanged(GameManager.Instance.HasWand);
        }
    }
    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.HasWand && Input.GetKeyDown(KeyCode.G))
        {
            DropWand();
        }
    }
    #endregion

    #region Event Handling
    private void HandleWandChanged(bool hasWand)
    {
        if (hasWand)
            wand.Equip();
        else
            wand.Unequip();
    }
    private void HandlePlayerDied()
    {
        wand.gameObject.SetActive(false);
    }
    #endregion

    #region Equip
    public void EquipWand()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.HasWand)
            return;

        GameManager.Instance.SetWand(true);
    }
    #endregion

    #region Drop
    public void DropWand()
    {
        if (GameManager.Instance == null)
            return;

        if (!GameManager.Instance.HasWand)
            return;

        Instantiate(
            wandPickupPrefab,
            dropPoint.position,
            Quaternion.identity
        );
        GameManager.Instance.SetWand(false);
    }
    #endregion
}