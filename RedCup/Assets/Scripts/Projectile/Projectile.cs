using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 10f;
    [Header("Tiempo de vida")]
    [SerializeField] private float lifeTime = 2f;
    [Header("Daño")]
    [SerializeField] private int damage = 1;
    [Header("Referencias")]
    private Rigidbody2D rb;
    [Header("Timer")]
    private float timer;
    #region Unity Lifecycle
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        timer = lifeTime;
    }
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnImpact();
            DisableProjectile();
        }
    }
    #endregion

    #region Inicializar
    public void Initialize(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
    }
    #endregion

    #region Colision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        SpawnImpact();
        DisableProjectile();
    }
    #endregion

    #region Disable
    private void DisableProjectile()
    {
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
    #endregion

    #region Spawn Impact
    private void SpawnImpact()
    {
        ParticlePool.Instance.GetParticle(transform.position);
    }
    #endregion
}
