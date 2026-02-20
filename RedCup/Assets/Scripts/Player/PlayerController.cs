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
    private ICommand dropCommand;
    [Header("Weapon")]
    [SerializeField] private Wand wand;
    [SerializeField] private PlayerWeaponController weaponController;

    private bool isStopped;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        shootCommand = new ShootCommand(wand);
        dropCommand = new DropWeaponCommand(weaponController);
    }
    #region Event Stop Movement
    private void OnEnable()
    {
        GameEvents.OnLevelStopped += HandleLevelStopped;
        GameEvents.OnLevelResumed += HandleLevelResumed;
        GameEvents.OnPlayerDied += HandlePlayerDied;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelStopped -= HandleLevelStopped;
        GameEvents.OnLevelResumed -= HandleLevelResumed;
        GameEvents.OnPlayerDied -= HandlePlayerDied;
    }

    private void HandleLevelStopped()
    {
        StopPlayer();
    }

    private void HandlePlayerDied()
    {
        StopPlayer();
    }

    private void HandleLevelResumed()
    {
        isStopped = false;
    }

    private void StopPlayer()
    {
        isStopped = true;
        currentCommand = null;
    }

    #endregion
    private void Update()
    {
        if (isStopped) return;


        // Logica de animacion
        playerAnimation.UpdateAnimation(playerInput.MoveInput, playerInput.IsRunning);
        Movimiento();
        Disparo();
        Drop();
    }
    private void FixedUpdate()
    {
        if (isStopped)
            return;

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
        if (!playerInput.ShootPressed)
            return;

        if (!GameManager.Instance.HasWand)
        {
            playerInput.ResetShoot();
            return;
        }

        shootCommand.Execute();
        playerInput.ResetShoot();
    }
    private void Drop()
    {
        if (playerInput.DropPressed)
        {
            dropCommand.Execute();
            playerInput.ResetDrop();
        }
    }
    #endregion
}
