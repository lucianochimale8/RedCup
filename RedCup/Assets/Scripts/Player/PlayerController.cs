using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Playing, Disabled }

    [Header("Referencias")]
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    
    [Header("Command")]
    private MoveCommand moveCommand;
    private RunCommand runCommand;
    private ShootCommand shootCommand;
    private DropWeaponCommand dropCommand;

    [Header("Weapon")]
    [SerializeField] private Wand wand;
    [SerializeField] private PlayerWeaponController weaponController;

    private PlayerState currentState = PlayerState.Playing;
    public PlayerState CurrentState => currentState;

    #region Unity Lifecycle
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();

        moveCommand = new MoveCommand(playerMovement, Vector2.zero);
        runCommand = new RunCommand(playerMovement, Vector2.zero);
        shootCommand = new ShootCommand(wand);
        dropCommand = new DropWeaponCommand(weaponController);
    }
    private void OnEnable() => GameEvents.OnPlayerHit += DisableControl;
    private void OnDisable() => GameEvents.OnPlayerHit -= DisableControl;
    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameState.Playing)
        {
            SetState(PlayerState.Disabled);
            return;
        }

        if (currentState == PlayerState.Disabled)
        {
            playerMovement.Stop();
            return;
        }

        // Logica de animacion
        playerAnimation.UpdateAnimation(playerInput.MoveInput, playerInput.IsRunning);
        
        ProcesarDisparo();
        ProcesarDrop();
    }
    private void FixedUpdate()
    {
        if (currentState == PlayerState.Disabled) return;

        Vector2 input = playerInput.MoveInput;

        if (playerInput.IsRunning)
        {
            runCommand.SetInput(input);
            runCommand.Execute();
        }
        else
        {
            moveCommand.SetInput(input);
            moveCommand.Execute();
        }
    }
    #endregion

    private void ProcesarDisparo()
    {
        if (!playerInput.ShootPressed) return;

        if (GameManager.Instance != null && GameManager.Instance.HasWand)
        {
            shootCommand.Execute();
        }
        playerInput.ResetShoot();
    }

    private void ProcesarDrop()
    {
        if (playerInput.DropPressed)
        {
            dropCommand.Execute();
            playerInput.ResetDrop();
        }
    }

    private void DisableControl()
    {
        SetState(PlayerState.Disabled);
        playerMovement.Stop();
    }

    public void SetState(PlayerState newState)
    {
        currentState = newState;
    }
}
