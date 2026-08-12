using UnityEngine;

public class MoveCommand : ICommand
{
    private PlayerMovement movement;
    private Vector2 input;
    public MoveCommand(PlayerMovement movement, Vector2 input)
    {
        this.movement = movement;
        this.input = input;
    }
    public void SetInput(Vector2 newInput) => input = newInput;
    public void Execute()
    {
        movement.SetStrategy(new WalkMovement());
        movement.Move(input);
    }
}
