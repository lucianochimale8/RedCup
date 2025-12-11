using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;

    private ICommand currentCommand;
    private ICommand shootCommand;

    [SerializeField] private Wand wand;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        shootCommand = new ShootCommand(wand);
    }

    private void Update()
    {
        // Logica de animacion
        playerAnimation.UpdateAnimation(playerInput.MoveInput, playerInput.IsRunning);

        // movimiento
        Vector2 moveInput = playerInput.MoveInput;

        if (playerInput.IsRunning)
        {
            currentCommand = new RunCommand(playerMovement, moveInput);
        }
        else
        {
            currentCommand = new MoveCommand(playerMovement, moveInput);
        }

        // disparo
        if (playerInput.ShootPressed)
        {
            shootCommand.Execute();
            playerInput.ResetShoot();
            Debug.Log("PlayerController ejecuta ShootCommand");
        }
    }

    private void FixedUpdate()
    {
        //playerMovement.Move(playerInput.MoveInput);
        currentCommand?.Execute();
    }
}
