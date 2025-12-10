using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerInput.IsRunning)
        {
            playerMovement.SetStrategy(new RunMovement());
        }
        else
        {
            playerMovement.SetStrategy(new WalkMovement());
        }
    }

    private void FixedUpdate()
    {
        playerMovement.Move(playerInput.MoveInput);
    }
}
