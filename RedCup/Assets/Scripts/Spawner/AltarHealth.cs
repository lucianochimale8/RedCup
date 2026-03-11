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

    [Header("AudioClip")]
    [SerializeField] private AudioClip altarHurt;
    [SerializeField] private float altarVolumen;

    private bool isDead;

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
        if (isDead) return;

        currentHealth -= amount;
        
        if (healthbar != null)
            healthbar.UpdateHealthBar(maxHealth, currentHealth);

        AudioManager.Instance.PlaySoundEffect(altarHurt, altarVolumen);

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
        isDead = true;

        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }
        //Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }
    #endregion

    #region Blink
    private IEnumerator Blink()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }
    #endregion
}
