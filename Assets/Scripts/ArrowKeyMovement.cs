using System;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;


public class ArrowKeyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float movement_speed = 4;
    private Vector2 directionFacing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 current_input = GetInput();
        AlignWithAxis(current_input);
        
        // set player's facing direction
        if (current_input != Vector2.zero)
        {
            directionFacing = current_input;
        }
        
        // Set velocity based on input 
        rb.linearVelocity = current_input * movement_speed;
        
    }

    Vector2 GetInput()
    {
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
    private void AlignWithAxis(Vector2 input)
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

    public Vector2 GetDirectionFacing()
    {
        return directionFacing;
    }
}
