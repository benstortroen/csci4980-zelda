using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public GameObject player;
    private Rigidbody2D rb;

    private bool moving = false;
    private int direction = 0;
    private int distance = 0;
    private Vector2 startPos;



    public float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SnapToGrid();
        startPos = (Vector2)transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            // Debug.Log("Moving");
            SnapToGrid();
            startPos = (Vector2)transform.position;
            int new_d;
            do
            {
                new_d = UnityEngine.Random.Range(0, 4);
                direction = UnityEngine.Random.Range(0, 4);
            } while (new_d != direction);
            direction = new_d;

            distance = UnityEngine.Random.Range(1, 8);
            switch (direction)
            {
                case 0:
                    rb.linearVelocity = new Vector2(speed, 0);
                    break;
                case 1:
                    rb.linearVelocity = new Vector2(-speed, 0);
                    break;
                case 2:
                    rb.linearVelocity = new Vector2(0, speed);
                    break;
                default:
                    rb.linearVelocity = new Vector2(0, -speed);
                    break;
            }
            moving = true;
        }
        else
        {
            float offset = Math.Abs(((Vector2)transform.position - startPos).magnitude);
            if (offset >= distance || (offset >= 2 && AlignedToPlayer()))
            {
                SnapToGrid();
                moving = false;
            }
        }
    }

    // Inflict damage to player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision");
        moving = false;
        GameObject other = collision.gameObject;

        if (other.tag == "Player")
        {
            player.GetComponent<HealthComponent>().DealDamage(1);
        }
        
    }

    private void SnapToGrid()
    {
        Vector2 temp = transform.position;
        float x_offset = transform.position.x % 0.5f;
        if (x_offset < 0.25f)
        {
            temp.x -= x_offset;
        }
        else
        {
            temp.x += 0.5f - x_offset;
        }


        float y_offset = transform.position.y % 0.5f;
        if (y_offset < 0.25f)
        {
            temp.y -= y_offset;
        }
        else
        {
            temp.y += 0.5f - y_offset;
        }

        transform.position = temp;
    }

    private bool AlignedToPlayer()
    {
        if (Math.Abs(transform.position.x - Math.Round(player.transform.position.x)) < 0.1f
            || Math.Abs(transform.position.y - Math.Round(player.transform.position.y)) < 0.1f)
        {
            return true;
        }
        return false;
    }
}
