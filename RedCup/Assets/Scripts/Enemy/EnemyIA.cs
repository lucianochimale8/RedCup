using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [Header("Velocidad del enenmigo")]
    [SerializeField] private float speed;
    [Header("Referencias")]
    private Transform playerTransform;
    private Animator animator;
    [Header("Banderas")]
    private bool isFacingRight = false;
    private bool isStopped = false;
    [Header("Ultima posicion del jugador")]
    private Vector2 lastPosition;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;
        animator = GetComponent<Animator>();

        lastPosition = transform.position;
    }

    private void Update()
    {
        if(isStopped)
        {
            animator.SetFloat("Speed",0f);
            return;
        }
        Follow();
        Flip();
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        // Calculamos velocidad real
        Vector2 velocity = ((Vector2)transform.position - lastPosition) / Time.deltaTime;

        animator.SetFloat("Speed", velocity.magnitude);
        lastPosition = transform.position;
    }
    #region Movimiento, Girar imagen, Parar movimiento
    private void Follow()
    {
        Vector2 playerDirection = (playerTransform.position - transform.position).normalized;
        transform.Translate(playerDirection * speed * Time.deltaTime);
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
