using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public enum WeaponState { Unequipped, Equipped }

    [Header("Arma")]
    [SerializeField] private Wand wand;
    [Header("GameObject Prefab")]
    [SerializeField] private GameObject wandPickupPrefab;
    [Header("Punto del drop")]
    [SerializeField] private Transform dropPoint;
    [Header("AudioClip")]
    [SerializeField] private AudioClip dropWand;
    [SerializeField] private float dropVolume = 0.8f;
    [SerializeField] private AudioClip equipWand;
    [SerializeField] private float equipVolume = 0.8f;

    private WeaponState currentState = WeaponState.Unequipped;
    public WeaponState CurrentState => currentState;

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
    #endregion

    #region Event Handling
    private void HandleWandChanged(bool hasWand)
    {
        if (hasWand)
        {
            currentState = WeaponState.Equipped;
            wand.Equip();
        }
        else
        {
            currentState = WeaponState.Unequipped;
            wand.Unequip();
        }
    }
    private void HandlePlayerDied()
    {
        currentState = WeaponState.Unequipped;
        wand.gameObject.SetActive(false);
    }
    #endregion

    #region Equip
    public void EquipWand()
    {
        if (GameManager.Instance == null || GameManager.Instance.HasWand) return;

        if (AudioManager.Instance != null && equipWand != null)
            AudioManager.Instance.PlaySoundEffect(equipWand, equipVolume);

        GameManager.Instance.SetWand(true);
    }
    #endregion

    #region Drop
    public void DropWand()
    {
        if (GameManager.Instance == null || !GameManager.Instance.HasWand) return;

        if (wandPickupPrefab != null && dropPoint != null)
        {
            Instantiate(wandPickupPrefab, dropPoint.position, Quaternion.identity);
        }

        if (AudioManager.Instance != null && dropWand != null)
            AudioManager.Instance.PlaySoundEffect(dropWand, dropVolume);

        GameManager.Instance.SetWand(false);
    }
    #endregion
}