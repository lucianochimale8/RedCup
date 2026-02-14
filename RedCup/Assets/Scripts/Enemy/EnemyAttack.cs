using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Solo dañar al Player
        // if (!collision.gameObject.CompareTag("Player")) return;

        Debug.Log("Enemy colisionó con: " + collision.gameObject.name);

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Debug.Log("Enemy hizo daño");
            damageable.TakeDamage(damage);
        }
    }
}
