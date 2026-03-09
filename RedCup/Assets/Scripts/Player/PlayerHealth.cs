using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour , IDamageable
{
    [Header("Damage Cooldown")]
    [SerializeField] private float damageCooldown = 0.5f;

    private bool canTakeDamage = true;
    private bool isDead = false;

    [Header("Referencias")]
    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerDied += Die;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= Die;
    }
    #region Damage
    public void TakeDamage(int amount)
    {

        if (!canTakeDamage || isDead) return;

        canTakeDamage = false;

        GameEvents.RaisePlayerHit();

        if (GameManager.Instance.Lives > 0)
        {
            playerAnimation.PlayHurt();
            StartCoroutine(DamageCooldown());
        }
    }
    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSecondsRealtime(damageCooldown);
        canTakeDamage = true;
    }
    #endregion

    #region Die
    private void Die()
    {
        if (isDead) return;

        isDead = true;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerInput != null)
            playerInput.enabled = false;

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        playerAnimation.PlayDie();

        Debug.Log("DIE()");
    }
    #endregion
}
