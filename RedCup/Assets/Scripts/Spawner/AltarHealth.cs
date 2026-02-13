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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

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
        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }

        GameEvents.OnAltarDestroyed?.Invoke();

        GameManager.Instance.OnAltarDestroyed();

        Destroy(transform.parent.gameObject);
    }

    private IEnumerator Blink()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }
}
