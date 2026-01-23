using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dieClip;
    [Header("Referencias")]
    private Animator animator;
    [Header("Si puede atacar")]
    private bool canTakeDamage = true;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canTakeDamage) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(DamageCooldown(0.5f));

            if (GameManager.Instance.Lives > 1)
                animator.SetTrigger("Hurt");
            else
                animator.SetTrigger("Die");

            GameManager.Instance.PlayerHit();
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
}
