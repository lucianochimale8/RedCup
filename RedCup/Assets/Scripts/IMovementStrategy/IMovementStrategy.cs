using UnityEngine;

public interface IMovementStrategy
{
    public Vector2 Move(Vector2 input, float baseSpeed);
}
