using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    private ICommand currentCommand;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Vector2 moveInput = playerInput.MoveInput;

        if (playerInput.IsRunning)
        {
            currentCommand = new RunCommand(playerMovement, moveInput);
        }
        else
        {
            currentCommand = new MoveCommand(playerMovement, moveInput);
        }
    }

    private void FixedUpdate()
    {
        //playerMovement.Move(playerInput.MoveInput);
        currentCommand?.Execute();
    }
}
