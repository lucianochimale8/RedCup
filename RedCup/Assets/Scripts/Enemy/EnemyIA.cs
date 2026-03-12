using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [Header("Velocidad del enenmigo")]
    [SerializeField] private float speed;
    [Header("Referencias")]
    private Transform playerTransform;
    private Animator animator;
    private Rigidbody2D rb;
    [Header("Banderas")]
    private bool isFacingRight = false;
    private bool isStopped;
    private bool isDead;

    #region Unity Lifecycle
    private void Awake()
    {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if (player != null)
            playerTransform = player.transform;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        GameEvents.OnLevelStopped += StopMovement;
        GameEvents.OnLevelResumed += ResumeMovement;
    }
    private void OnDisable()
    {
        GameEvents.OnLevelStopped -= StopMovement;
        GameEvents.OnLevelResumed -= ResumeMovement;
    }
    private void Start()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
        {
            StopMovement();
        }
    }
    private void Update()
    {
        if (isDead) return;

        if (isStopped)
        {
            animator.SetFloat("Speed",0f);
            return;
        }
        Flip();
        UpdateAnimation();
    }
    // para control de fisicas FixedUpdate
    private void FixedUpdate()
    {
        if (isDead) return;

        if (isStopped)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        
        Follow();
    }
    #endregion

    #region Animation
    private void UpdateAnimation()
    {
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    }
    #endregion
    #region Movimiento, Girar imagen, Parar movimiento
    private void Follow()
    {
        float distance = Vector2.Distance(rb.position, playerTransform.position);

        if (distance < 0.5f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 playerDirection =
            ((Vector2)playerTransform.position - rb.position).normalized;
        
        rb.linearVelocity = playerDirection * speed;
    }
    private void Flip()
    {
        bool isPlayerRight = playerTransform.position.x < transform.position.x;

        if ((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            Vector3 scale = transform.localScale; // Variable referenciad de la escala
            scale.x *= -1f; // invertir escala
            transform.localScale = scale; // establecer nueva escala
            isFacingRight = !isFacingRight; // invertir si se ha dado vuelta
        }
    }
    #endregion

    #region Stop & Resume
    public void StopMovement()
    {
        isStopped = true;
        rb.linearVelocity = Vector2.zero;
    }
    public void ResumeMovement()
    {
        if (isDead) return;

        isStopped = false;
    }
    #endregion
    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
    }
}
