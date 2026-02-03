using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    // Para reconocer cuando se esta corriendo
    public bool IsRunning { get; private set; }

    public bool ShootPressed { get; private set; }

    public bool DropPressed { get; private set; }

    private void Update()
    {
        // ejes verticales y horizontales
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        // solo este script puede escribir el movimiento
        MoveInput = new Vector2(x, y).normalized;
        // Shift para correr
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        // click derecho
        if (Input.GetMouseButtonDown(1))
            ShootPressed = true;
        if (Input.GetKeyDown(KeyCode.G))
            DropPressed = true;
    }
    public void ResetShoot()
    {
        ShootPressed = false;
    }
    public void ResetDrop()
    {
        DropPressed = false;
    }
}
