using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    // Para reconocer cuando se esta corriendo
    public bool IsRunning { get; private set; }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        // solo este script puede escribir el movimiento
        MoveInput = new Vector2(x, y).normalized;
        // Shift para correr
        IsRunning = Input.GetKey(KeyCode.LeftShift);
    }
}
