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
    private PlayerInput playerInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }   
    #region Damage & Die
    public void TakeDamage(int amount)
    {

        if (!canTakeDamage || isDead) return;

        canTakeDamage = false;

        GameEvents.RaisePlayerHit();

        if (GameManager.Instance.Lives <= 1)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
            StartCoroutine(DamageCooldown());
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        if (movement != null)
            movement.enabled = false;

        if (playerInput != null)
            playerInput.enabled = false;

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        animator.SetTrigger("Die");

        Debug.Log("DIE()");
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
    #endregion
}
