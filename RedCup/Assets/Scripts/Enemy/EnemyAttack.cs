using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Para solo reconocer al player
        // que no los enemigos no dañen los altares ya q comparten interfaz
        
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;


        Debug.Log("Enemy colisionó con: " + collision.gameObject.name);

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Debug.Log("Enemy hizo daño");
            damageable?.TakeDamage(damage);
        }
    }
}
