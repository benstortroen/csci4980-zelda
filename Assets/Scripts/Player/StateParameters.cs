using NUnit.Framework;
using UnityEngine;

// Keep track of state variables such as:
// direction
// isAttacking

public class StateParameters : MonoBehaviour
{
    // state parameters to track
    private Vector3 facingDirection = Vector3.down;
    private bool isMoving = false;
    private bool isAttacking = false;

    private bool knockbackMode = false;



    // Facing Direction
    public void SetFacingDirection(Vector3 dir)
    {
        facingDirection = dir;
    }

    public Vector3 GetFacingDirection()
    {
        return facingDirection;
    }


    // Is Moving
    public void SetIsMoving(bool _isMoving)
    {
        isMoving = _isMoving;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }



    // Is Attacking
    public void SetIsAttacking(bool _isAttacking)
    {
        isAttacking = _isAttacking;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

    // Knockback Mode
    public void SetKnockbackMode(bool _knockbackMode)
    {
        knockbackMode = _knockbackMode;
    }

    public bool GetKnockbackMode()
    {
        return knockbackMode;
    }

}
