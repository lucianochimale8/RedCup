using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Referencias")]
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void UpdateAnimation(Vector2 input, bool isRunning)
    {
        float speed = input.magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsRunning", isRunning);
    }
}
