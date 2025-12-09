using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;

    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 input) 
    {
        rb.linearVelocity = input * moveSpeed;
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
}
