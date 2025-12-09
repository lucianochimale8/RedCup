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

    private void FixedUpdate()
    {
        playerMovement.Move(playerInput.MoveInput);
    }
}
