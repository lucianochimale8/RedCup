using UnityEngine;


public class PlayerCollision : MonoBehaviour
{
    private IDamageable damageable;

    private void Awake()
    {
        damageable = GetComponent<IDamageable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageable.TakeDamage(1);
        }
    }
}
