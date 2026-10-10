using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;


public class ArrowKeyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    StateParameters stateParameters;

    [SerializeField] private float movement_speed = 4;

    private Vector2 kBStart;

    [SerializeField] float knockbackSpeed = 10;
    [SerializeField] float knockbackDist = 2;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateParameters = GetComponent<StateParameters>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stateParameters.GetKnockbackMode())
        {
            Vector2 current_input = GetInput();
            // Align player position with grid
            SnapToGrid(current_input);
            // Set velocity based on input
            rb.linearVelocity = current_input * movement_speed;
            // Set Player's State Paramters
            // set player's moving state
            stateParameters.SetIsMoving(current_input != Vector2.zero);
            // set player's facing direction
            if (current_input != Vector2.zero)
            {
                // assume player has StateParameters component to send facing direction to
                gameObject.GetComponent<StateParameters>().SetFacingDirection(current_input);
            }
        }
        else
        {
            float offset = Math.Abs(((Vector2)transform.position - kBStart).magnitude);
            if (offset >= knockbackDist)
            {
                stateParameters.SetKnockbackMode(false);
            }
        }
    }

    public void Knockback(Vector2 dir)
    {
        if (!stateParameters.GetKnockbackMode())
        {
            stateParameters.SetKnockbackMode(true);
            rb.linearVelocity = dir * knockbackSpeed;
            kBStart = transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            stateParameters.SetKnockbackMode(false);
        }
    }

    Vector2 GetInput()
    {
        // Do not move if player is attacking
        if (stateParameters.GetIsAttacking())
        {
            return Vector2.zero;
        }

        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        // Only allow one axis of movement at a time
        if (horizontal_input != 0f)
        {
            vertical_input = 0f;
        }
        if (vertical_input != 0f)
        {
            horizontal_input = 0f;
        }

        return new Vector2(horizontal_input, vertical_input);
    }

    // Snap player to the opposite axis they are moving along
    // if player is moving horizontally, snap to nearest y grid
    // if player is moving vertically, snap to nearest x grid
    // Each grid step is 0.5 units
    private void SnapToGrid(Vector2 input)
    {
        Vector3 temp_pos = transform.position;

        // Player is moving along x axis
        if (input.x != 0)
        {
            float offset = temp_pos.y % 0.5f;

            // snap to nearest lower grid position
            temp_pos.y -= offset;

            // check if player was closer to nearest higher grid position
            // move to nearest higher grid position
            if (offset > 0.25)
            {
                temp_pos.y += 0.5f;
            }
        }

        // Player is moving along y axis
        if (input.y != 0)
        {
            float offset = temp_pos.x % 0.5f;

            // snap to nearest lower grid position
            temp_pos.x -= offset;

            // check if player was closer to nearest higher grid position
            // move to nearest higher grid position
            if (offset > 0.25)
            {
                temp_pos.x += 0.5f;
            }
        }

        transform.position = temp_pos;
    }
}
