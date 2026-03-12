using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Referencias")]
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private Rigidbody2D rb;
    [Header("Command")]
    private ICommand currentCommand;
    private ICommand shootCommand;
    private ICommand dropCommand;
    [Header("Weapon")]
    [SerializeField] private Wand wand;
    [SerializeField] private PlayerWeaponController weaponController;
    // bloqueo de movimiento
    private bool canMove = true;

    #region Unity Lifecycle
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();

        shootCommand = new ShootCommand(wand);
        dropCommand = new DropWeaponCommand(weaponController);
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerHit += StopPlayer;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerHit -= StopPlayer;
    }
    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Logica de animacion
        playerAnimation.UpdateAnimation(playerInput.MoveInput, playerInput.IsRunning);
        Movimiento();
        Disparo();
        Drop();
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
        {
            rb.linearVelocity = Vector2.zero;
            return; 
        }

        if (!canMove)
        {
            Debug.Log("no me puedo mover");
            currentCommand = null;
            return;
        }

        if(canMove)
            currentCommand?.Execute();
    }
    #endregion

    #region Movimiento
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
    #endregion

    #region Disparo
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
    #endregion

    #region Drop
    private void Drop()
    {
        if (playerInput.DropPressed)
        {
            dropCommand.Execute();
            playerInput.ResetDrop();
        }
    }
    #endregion

    #region Stop
    private void StopPlayer()
    {
        canMove = false;

        rb.linearVelocity = Vector2.zero;

        currentCommand = null;
    }
    #endregion
}
