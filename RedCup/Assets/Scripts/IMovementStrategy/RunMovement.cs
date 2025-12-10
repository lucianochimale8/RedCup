using UnityEngine;

public class RunMovement : IMovementStrategy
{
    [SerializeField] private float runBoost = 1.8f;
    public Vector2 Move(Vector2 input, float baseSpeed)
    {
        return input * baseSpeed * runBoost; // correr = x1.8
    }
}
