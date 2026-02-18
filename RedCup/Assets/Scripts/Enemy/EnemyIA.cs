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

    #region Unity Lifecycle
    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;
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

    private void Update()
    {
        if(isStopped)
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
        speed = 0f;
    }
    public void ResumeMovement()
    {
        isStopped = false;
    }
    #endregion
}
