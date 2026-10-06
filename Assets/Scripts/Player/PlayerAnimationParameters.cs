using UnityEngine;

// Update Animation parameters using variables from StateParameters component

public class PlayerAnimationParameters : MonoBehaviour
{
    StateParameters stateParameters;
    Animator animator;

    void Start()
    {
        stateParameters = gameObject.GetComponent<StateParameters>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 facingDirection = stateParameters.GetFacingDirection();
        animator.SetFloat("horizontal_input", facingDirection.x);
        animator.SetFloat("vertical_input", facingDirection.y);

        // play animation only when player is moving
        if (stateParameters.GetIsMoving())
        {
            animator.speed = 1.0f;
        }
        else
        {
            animator.speed = 0.0f;
        }

        bool isAttacking = stateParameters.GetIsAttacking();
        animator.SetBool("is_attacking", isAttacking);

    }
}
