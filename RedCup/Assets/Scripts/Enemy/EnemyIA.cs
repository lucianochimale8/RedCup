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
    private bool isStopped = false;
    [Header("Ultima posicion del jugador")]
    private Vector2 lastPosition;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        lastPosition = transform.position;
    }

    private void Update()
    {
        if(isStopped)
        {
            animator.SetFloat("Speed",0f);
            return;
        }
        //Follow();
        Flip();
        UpdateAnimation();
    }
    // para control de fisicas FixedUpdate
    private void FixedUpdate()
    {
        Follow();
    }

    private void UpdateAnimation()
    {
        // Calculamos velocidad real
        //Vector2 velocity = ((Vector2)transform.position - lastPosition) / Time.deltaTime;

        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
        lastPosition = transform.position;
    }
    #region Movimiento, Girar imagen, Parar movimiento
    private void Follow()
    {
        Vector2 playerDirection =
            ((Vector2)playerTransform.position - rb.position).normalized;
        
        rb.linearVelocity = playerDirection * speed;

       //rb.MovePosition(rb.position + playerDirection * speed * Time.fixedDeltaTime);

        // Translate hacia que los enemigos ignoren las fisicas y traspasen las paredes
        //transform.Translate(playerDirection * speed * Time.deltaTime);
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
    public void StopMovement()
    {
        isStopped = true;
        speed = 0f;
    }
    #endregion
}
