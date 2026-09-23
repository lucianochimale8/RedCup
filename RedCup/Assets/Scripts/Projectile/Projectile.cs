using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum ProjectileState { Inactive, Active }

    [Header("Movimiento")]
    [SerializeField] private float speed = 10f;
    [Header("Tiempo de vida")]
    [SerializeField] private float lifeTime = 2f;
    [Header("Daño")]
    [SerializeField] private int damage = 1;
    
    private Rigidbody2D rb;
    private float timer;

    private ProjectileState currentState = ProjectileState.Inactive;
    public ProjectileState CurrentState => currentState;

    #region Unity Lifecycle
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        currentState = ProjectileState.Active;
        timer = lifeTime;
    }
    private void Update()
    {
        if (currentState != ProjectileState.Active) return;

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
        if (rb != null)
            rb.linearVelocity = direction.normalized * speed;
    }
    #endregion

    #region Colision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState != ProjectileState.Active) return;

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
        currentState = ProjectileState.Inactive;

        if (ParticlePool.Instance != null)
            ParticlePool.Instance.GetParticle(transform.position);

        if (rb != null)
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
