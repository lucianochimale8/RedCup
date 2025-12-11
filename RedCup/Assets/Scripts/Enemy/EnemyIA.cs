using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform playerTransform;
    private Animator animator;
    private bool isFacingRight = false;

    private Vector2 lastPosition;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;
        animator = GetComponent<Animator>();

        lastPosition = transform.position;
    }

    private void Update()
    {
        Follow();
        Flip();
        UpdateAnimation();
    }

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

    private void UpdateAnimation()
    {
        // Calculamos velocidad real
        Vector2 velocity = ((Vector2)transform.position - lastPosition) / Time.deltaTime;
        float speedMagnitude = velocity.magnitude;

        if (animator != null)
        {
            animator.SetFloat("Speed", speedMagnitude);
        }

        lastPosition = transform.position;
    }
}
