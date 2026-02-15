using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour , IDamageable
{
    [Header("Damage Cooldown")]
    [SerializeField] private float damageCooldown = 0.5f;

    private bool canTakeDamage = true;
    private bool isDead = false;

    [Header("Referencias")]
    private Animator animator;
    private PlayerMovement movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }   
    #region Damage and Die
    public void TakeDamage(int amount)
    {
        Debug.Log("TakeDamage llamado");

        if (!canTakeDamage)
            Debug.Log("NO puede recibir daño (cooldown)");

        if (isDead)
            Debug.Log("Player ya está muerto");

        if (!canTakeDamage || isDead) return;

        canTakeDamage = false;

        if (GameManager.Instance.Lives > 1)
        {
            animator.SetTrigger("Hurt");
            GameEvents.RaisePlayerHit();
            StartCoroutine(DamageCooldown());
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (movement != null)
            movement.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // frena movimiento
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic; // opcional, lo congela totalmente
        }

        animator.SetTrigger("Die");
        GameEvents.RaisePlayerDied();
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
    #endregion
}
