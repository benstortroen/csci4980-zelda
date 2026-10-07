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

        // moving state, drives the Idle <-> Walk transitions
        animator.SetBool("is_moving", stateParameters.GetIsMoving());

        // attacking state
        animator.SetBool("is_attacking", stateParameters.GetIsAttacking());

    }
}
