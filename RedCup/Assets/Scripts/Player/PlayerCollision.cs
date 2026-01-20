using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private AudioClip dieClip;
    private Animator animator;
    private bool canTakeDamage = true;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

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

    private IEnumerator DamageCooldown(float time)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(time);
        canTakeDamage = true;
    }

}
