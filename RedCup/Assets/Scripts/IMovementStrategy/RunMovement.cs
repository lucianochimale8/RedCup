using UnityEngine;

public class RunMovement : IMovementStrategy
{
    private const float RunBoost = 1.5f;
    public Vector2 Move(Vector2 input, float baseSpeed) => input * baseSpeed * RunBoost;
}
