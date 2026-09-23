using UnityEngine;

public class RunCommand : ICommand
{
    private PlayerMovement movement;
    private Vector2 input;

    public RunCommand(PlayerMovement movement, Vector2 input)
    {
        this.movement = movement;
        this.input = input;
    }
    public void SetInput(Vector2 newInput) => input = newInput;
    public void Execute()
    {
        movement.SetStrategy(new RunMovement());
        movement.Move(input);
    }
}
