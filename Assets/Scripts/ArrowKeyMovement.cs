using System;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class ArrowKeyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float movement_speed = 4;
    public float adjust_rate = 1f;

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
        DirectionalCenter(current_input);
    }

    private void DirectionalCenter(Vector2 input)
    {
        Vector3 temp_pos = transform.position;
        if (input.x != 0)
        {
            // Debug.Log("Locking");
            float offset = temp_pos.y % 1f;
            if (offset <= .5f)
            {
                temp_pos.y -= adjust_rate * Time.deltaTime;
            }
            else
            {
                temp_pos.y += adjust_rate * Time.deltaTime;
            }
        }
        else if (input.y != 0)
        {
            float offset = temp_pos.x % 1f;
            if (offset <= .5f)
            {
                temp_pos.x -= adjust_rate * Time.deltaTime;
            }
            else
            {
                temp_pos.x += adjust_rate * Time.deltaTime;
            }
        }
        transform.position = temp_pos;
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
