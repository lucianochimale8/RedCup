using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public enum AIState { Chasing, Stopped, Dead }

    [Header("Velocidad del enenmigo")]
    [SerializeField] private float speed;

    private Transform playerTransform;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isFacingRight = false;

    private AIState currentState = AIState.Chasing;
    public AIState CurrentState => currentState;

    #region Unity Lifecycle
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        currentState = AIState.Chasing;
        GameEvents.OnLevelStopped += StopMovement;
        GameEvents.OnLevelResumed += ResumeMovement;

        if (playerTransform == null)
        {
            PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
            if (player != null) playerTransform = player.transform;
        }
    }
    private void OnDisable()
    {
        GameEvents.OnLevelStopped -= StopMovement;
        GameEvents.OnLevelResumed -= ResumeMovement;
    }
    private void Update()
    {
        if (currentState != AIState.Chasing)
        {
            if (animator != null) animator.SetFloat("Speed", 0f);
            return;
        }

        Flip();
        if (animator != null) animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    }
    // para control de fisicas FixedUpdate
    private void FixedUpdate()
    {
        if (currentState != AIState.Chasing)
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
        if (playerTransform == null) return;

        float distance = Vector2.Distance(rb.position, playerTransform.position);
        if (distance < 0.5f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)playerTransform.position - rb.position).normalized;
        rb.linearVelocity = direction * speed;
    }
    private void Flip()
    {
        if (playerTransform == null) return;

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
        if (currentState == AIState.Dead) return;
        currentState = AIState.Stopped;
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }
    public void ResumeMovement()
    {
        if (currentState == AIState.Dead) return;
        currentState = AIState.Chasing;
    }
    #endregion
    public void SetDead()
    {
        currentState = AIState.Dead;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
    }
}
