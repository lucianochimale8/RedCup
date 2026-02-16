using UnityEngine;
using System.Collections;

public class AltarHealth : MonoBehaviour , IDamageable
{
    [Header("Vida Altar")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("Key")]
    [SerializeField] private GameObject dropPrefab;

    [Header("UI")]
    [SerializeField] private Healthbar healthbar;

    [Header("Referencias")]
    private SpriteRenderer spriteRenderer;

    private bool isDead;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        
        if (healthbar != null)
            healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(Blink());
    }

    private void Die()
    {
        isDead = true;

        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }

        GameEvents.RaiseAltarDestroyed();

        Destroy(transform.parent.gameObject);
    }

    private IEnumerator Blink()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }
}
