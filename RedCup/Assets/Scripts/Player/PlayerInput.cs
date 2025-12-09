using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        MoveInput = new Vector2(x, y).normalized;
    }
}
