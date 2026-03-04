using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Vida Enemigo")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float hurtDuration = 0.2f;

    private int currentHealth;
    private bool isDead = false;

    [Header("Referencias")]
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private EnemyIA enemyIA;

    [Header("UI")]
    [SerializeField] private Healthbar healthbar;

    private Spawner spawner;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyIA = GetComponent<EnemyIA>();

        currentHealth = maxHealth;
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
        if (isDead) return;

        currentHealth -= damage;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(Blink());
    }
    #endregion
    #region Die y Blink
    private void Die()
    {
        isDead = true;

        enemyIA.StopMovement();

        animator.SetTrigger("Die");

        GetComponent<Collider2D>().enabled = false;

        GameEvents.RaiseEnemyKilled();

        if (gameObject.CompareTag("EnemyAfk"))
            gameObject.SetActive(false);

        if (gameObject.CompareTag("Enemy"))
            spawner.ReturnEnemyToPool(gameObject);
    }
    private IEnumerator Blink()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(hurtDuration);
        spriteRenderer.color = Color.white;
    }
    #endregion
    #region Reset del enemigo

    public void ResetEnemy()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    #endregion
}
