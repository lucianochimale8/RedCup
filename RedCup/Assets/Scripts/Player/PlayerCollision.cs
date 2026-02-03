using UnityEngine;


public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = GetComponent<IDamageable>();
        if (damageable == null) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageable.TakeDamage(1);
        }
    }
}
