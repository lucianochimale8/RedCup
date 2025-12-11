using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float hurtDuration = 0.2f;

    private int health;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private AudioClip enemyDieClip, enemyHitClip;

    private bool isHurt = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null && !isHurt)
        {
            TakeDamage();
            //projectile.gameObject.SetActive(false);
        }
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            AudioManager.Instance.PlaySoundEffect(enemyDieClip, 0.5f);
            GameManager.Instance.DecreaseEnemiesLeft();
            Destroy(gameObject);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(HurtRoutine());
            AudioManager.Instance.PlaySoundEffect(enemyHitClip, 0.5f);
        }
    }

    private IEnumerator HurtRoutine()
    {
        isHurt = true;

        StartCoroutine(Blink(hurtDuration));

        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;
    }

    private IEnumerator Blink(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = Color.white;
    }
}
