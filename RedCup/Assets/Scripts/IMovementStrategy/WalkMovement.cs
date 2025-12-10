using UnityEngine;

public class WalkMovement : IMovementStrategy
{
    public Vector2 Move(Vector2 input, float baseSpeed)
    {
        return input * baseSpeed; // velocidad normal
    }
}
