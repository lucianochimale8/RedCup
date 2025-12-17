using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida Enemigo")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float hurtDuration = 0.2f;

    private int health;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.DecreaseEnemiesLeft();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(Blink(hurtDuration));
    }
    private IEnumerator Blink(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = Color.white;
    }
}
