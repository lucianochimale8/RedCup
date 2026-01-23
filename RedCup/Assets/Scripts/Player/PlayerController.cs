using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Referencias")]
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    [Header("Command")]
    private ICommand currentCommand;
    private ICommand shootCommand;
    [Header("Weapon")]
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
        Movimiento();
        Disparo();
    }
    private void FixedUpdate()
    {
        currentCommand?.Execute();
    }
    #region Movimiento y Disparo del jugador
    private void Movimiento()
    {
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
    }
    private void Disparo()
    {
        // disparo
        if (playerInput.ShootPressed)
        {
            shootCommand.Execute();
            playerInput.ResetShoot();
            Debug.Log("PlayerController ejecuta ShootCommand");
        }
    }
    #endregion
}
