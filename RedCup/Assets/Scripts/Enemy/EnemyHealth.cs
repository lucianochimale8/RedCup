using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public enum HealthState { Alive, Hurt, Dead }

    [Header("Vida Enemigo")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float hurtDuration = 0.2f;

    private int currentHealth;
    private HealthState currentState = HealthState.Alive;

    [Header("Referencias")]
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private EnemyIA enemyIA;
    private Collider2D enemyCollider;

    [Header("UI")]
    [SerializeField] private Healthbar healthbar;
    [Header("AudioClip")]
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private float volume;

    private Spawner spawner;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyIA = GetComponent<EnemyIA>();
        enemyCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        ResetEnemy();
    }
    public void SetSpawner(Spawner owner)
    {
        spawner = owner;
    }

    #region Tomar daño
    public void TakeDamage(int damage)
    {
        if (currentState == HealthState.Dead) return;

        currentHealth -= damage;
        if (healthbar != null) healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(BlinkRoutine());
    }
    #endregion
    #region Die y Blink
    private void Die()
    {
        currentState = HealthState.Dead;

        if (healthbar != null) healthbar.gameObject.SetActive(false);
        if (enemyIA != null) enemyIA.SetDead();
        if (enemyCollider != null) enemyCollider.enabled = false;
        if (animator != null) animator.SetTrigger("Die");

        if (AudioManager.Instance != null && deathClip != null)
            AudioManager.Instance.PlaySoundEffect(deathClip, volume);

        GameEvents.RaiseEnemyKilled();
        StartCoroutine(FadeAndRecycleRoutine());
    }
    private IEnumerator BlinkRoutine()
    {
        currentState = HealthState.Hurt;
        if (spriteRenderer != null) spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(hurtDuration);
        if (spriteRenderer != null) spriteRenderer.color = Color.white;
        if (currentState != HealthState.Dead) currentState = HealthState.Alive;
    }
    #endregion

    #region Reset del enemigo
    public void ResetEnemy()
    {
        currentHealth = maxHealth;
        currentState = HealthState.Alive;
        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = 1f;
            spriteRenderer.color = c;
        }
        if (enemyCollider != null) enemyCollider.enabled = true;
        if (healthbar != null) healthbar.gameObject.SetActive(true);
    }
    #endregion
    private IEnumerator FadeAndRecycleRoutine()
    {
        float time = 1.5f;
        float elapsed = 0;
        Color color = spriteRenderer.color;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / time);
            spriteRenderer.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        if (spawner != null)
            spawner.ReturnEnemyToPool(gameObject);
        else
            Destroy(gameObject);
    }
}
