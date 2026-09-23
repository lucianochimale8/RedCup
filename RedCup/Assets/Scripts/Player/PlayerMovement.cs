using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidad")]
    [SerializeField] private float moveSpeed = 5f;

    
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    public IMovementStrategy movementStrategy { get; private set; }

    #region Unity Lifecycle
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Walk por defecto
        movementStrategy = new WalkMovement();
    }
    #endregion

    #region Move, Flip
    public void Move(Vector2 input) 
    {
        // Velocidad
        Vector2 finalVelocity = movementStrategy.Move(input, moveSpeed);
        rb.linearVelocity = finalVelocity;
        Flip(input);
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
    #endregion

    #region SetStrategy
    // Metodo para cambiar entre estrategias
    public void SetStrategy(IMovementStrategy newStrategy)
    {
        movementStrategy = newStrategy;
    }
    #endregion
    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
