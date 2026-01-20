using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida Enemigo")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float hurtDuration = 0.2f;

    private int health;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private EnemyIA enemyIA;

    [SerializeField] private Healthbar healthbar;

    private bool isDead = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyIA = GetComponent<EnemyIA>();

        health = maxHealth;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            TakeDamage();
            projectile.gameObject.SetActive(false);
        }
    }
    public void TakeDamage()
    {
        if (isDead) return;

        health--;
        healthbar.UpdateHealthBar(maxHealth, health);

        if (health <= 0)
        {
            Die();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(Blink(hurtDuration));
    }

    private void Die()
    {
        isDead = true;

        enemyIA.StopMovement();
        animator.SetTrigger("Die");
        GetComponent<Collider2D>().enabled = false;

        GameManager.Instance.DecreaseEnemiesLeft();
        Destroy(gameObject, 0.4f);
    }

    private IEnumerator Blink(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = Color.white;
    }
}
