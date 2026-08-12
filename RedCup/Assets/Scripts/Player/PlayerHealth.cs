using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour , IDamageable
{
    public enum HealthState { Alive, Vulnerable, Invulnerable, Dead }

    [Header("Damage Cooldown")]
    [SerializeField] private float damageCooldown = 0.5f;

    [Header("AudioClip")]
    [SerializeField] private AudioClip hurtClip, dieClip;
    [SerializeField] private float hurtVolume, dieVolume;

    private HealthState currentState = HealthState.Alive;
    public HealthState CurrentState => currentState;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    
    #region Unity Lifecycle
    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void OnEnable() => GameEvents.OnPlayerDied += Die;
    private void OnDisable() => GameEvents.OnPlayerDied -= Die;
    #endregion

    #region Damage
    public void TakeDamage(int amount)
    {
        if (currentState == HealthState.Invulnerable || currentState == HealthState.Dead)
            return;

        GameEvents.RaisePlayerHit();

        if (GameManager.Instance != null && GameManager.Instance.Lives > 0)
        {
            if (playerAnimation != null) playerAnimation.PlayHurt();
            if (AudioManager.Instance != null && hurtClip != null)
                AudioManager.Instance.PlaySoundEffect(hurtClip, hurtVolume);

            StartCoroutine(DamageCooldownRoutine());
        }
    }
    private IEnumerator DamageCooldownRoutine()
    {
        currentState = HealthState.Invulnerable;
        yield return new WaitForSecondsRealtime(damageCooldown);
        if (currentState != HealthState.Dead)
        {
            currentState = HealthState.Alive;
        }
    }
    #endregion

    #region Die
    private void Die()
    {
        if (currentState == HealthState.Dead) return;

        currentState = HealthState.Dead;

        if (playerMovement != null) playerMovement.enabled = false;
        if (playerInput != null) playerInput.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        if (playerAnimation != null) playerAnimation.PlayDie();
        if (AudioManager.Instance != null && dieClip != null)
            AudioManager.Instance.PlaySoundEffect(dieClip, dieVolume);
    }
    #endregion
}
