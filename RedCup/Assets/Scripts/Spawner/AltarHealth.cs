using UnityEngine;
using System.Collections;

public class AltarHealth : MonoBehaviour , IDamageable
{
    public enum AltarState
    {
        Idle,
        Hurt,
        Dead
    }

    [Header("Vida Altar")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("Key")]
    [SerializeField] private GameObject dropPrefab;

    [Header("UI")]
    [SerializeField] private Healthbar healthbar;

    [Header("AudioClip")]
    [SerializeField] private AudioClip altarHurt;
    [SerializeField] private float altarVolumen;

    private AltarState currentState = AltarState.Idle;
    public AltarState CurrentState => currentState;

    private SpriteRenderer spriteRenderer;

    #region Unity Lifecycle
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }
    #endregion

    #region Take Damage
    public void TakeDamage(int amount)
    {
        if (currentState == AltarState.Dead)
            return;

        currentHealth -= amount;
        
        if (healthbar != null)
        {
            healthbar.UpdateHealthBar(maxHealth, currentHealth);
        }

        if (AudioManager.Instance != null && altarHurt != null)
        {
            AudioManager.Instance.PlaySoundEffect(altarHurt, altarVolumen);
        }

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(Blink());
    }
    #endregion

    #region Die
    private void Die()
    {
        currentState = AltarState.Dead;

        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    #endregion

    #region Blink
    private IEnumerator Blink()
    {
        currentState = AltarState.Hurt;

        if (spriteRenderer != null)
            spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;

        if (currentState != AltarState.Dead)
        {
            currentState = AltarState.Idle;
        }
    }
    #endregion
}
