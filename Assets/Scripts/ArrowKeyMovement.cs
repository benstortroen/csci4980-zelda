using System;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class ArrowKeyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float movement_speed = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 current_input = GetInput();
        // Set speed
        rb.linearVelocity = current_input * movement_speed;

        //Temp version of transform to make editing easier
        Vector3 currentPos = transform.position;

        // Floor everything to a .5 multiple when within .1
        if (currentPos.x % 0.5f < 0.1)
        {
            float x_dir = current_input.x;
            currentPos.x += x_dir * (currentPos.x % 0.5f);
        }

        if (currentPos.y % 0.5f < 0.1)
        {
            float y_dir = current_input.y;
            currentPos.y += y_dir * (currentPos.y % 0.5f);
        }
        // Reasign transform
        transform.position = currentPos;
    }

    Vector2 GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        if (horizontal_input != 0.0f)
        {
            vertical_input = 0.0f;
        }

        return new Vector2(horizontal_input, vertical_input);
    }
}
