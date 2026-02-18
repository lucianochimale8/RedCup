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
    #region Damage & Die
    public void TakeDamage(int amount)
    {

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
            rb.simulated = false;
        }

        animator.SetTrigger("Die");
        GameEvents.RaisePlayerHit();
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
    #endregion
}
