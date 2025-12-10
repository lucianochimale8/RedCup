using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;

    private bool isFacingRight = true;

    // Movement Strategy
    public IMovementStrategy movementStrategy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Walk por defecto
        movementStrategy = new WalkMovement();
    }

    public void Move(Vector2 input) 
    {
        // Velocidad
        Vector2 finalVelocity = movementStrategy.Move(input, moveSpeed);
        rb.linearVelocity = finalVelocity;
        Flip(input);
    }
    // Metodo para cambiar entre estrategias
    public void SetStrategy(IMovementStrategy newStrategy)
    {
        movementStrategy = newStrategy;
    }

    private void Flip(Vector2 input)
    {
        if (input.x != 0)
        {
            if ((isFacingRight && input.x < 0f) || (!isFacingRight && input.x > 0f))
            {
                Vector3 scale = transform.localScale; // Variable referenciad de la escala
                scale.x *= -1f; // invertir escala
                transform.localScale = scale; // establecer nueva escala
                isFacingRight = !isFacingRight; // invertir si se ha dado vuelta
            }
        }

    }
}
