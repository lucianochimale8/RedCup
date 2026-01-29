using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dieClip;
    [Header("Referencias")]
    private Animator animator;
    [Header("Referencias parar movimiento")]
    private MonoBehaviour movementScript;
    [Header("Si puede atacar")]
    private bool canTakeDamage = true;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movementScript = GetComponent<PlayerMovement>();
    }
    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canTakeDamage || isDead) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            canTakeDamage = false;

            if (GameManager.Instance.Lives > 1)
            {
                animator.SetTrigger("Hurt");
                StartCoroutine(DamageCooldown(0.5f));
                GameManager.Instance.PlayerHit();
            }
            else
            {
                StartCoroutine(DisablePlayer());
            }          
        }
    }
    #endregion
    #region Damage
    private IEnumerator DamageCooldown(float time)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(time);
        canTakeDamage = true;
    }
    #endregion
    #region Detener al jugador
    private IEnumerator DisablePlayer()
    {
        isDead = true;

        if (movementScript != null)
            movementScript.enabled = false;

        animator.SetTrigger("Die");

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.PlayerHit();
    }
    #endregion
}
