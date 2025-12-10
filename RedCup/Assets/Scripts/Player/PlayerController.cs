using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;

    private ICommand currentCommand;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        // Logica de animacion
        playerAnimation.UpdateAnimation(playerInput.MoveInput, playerInput.IsRunning);

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
