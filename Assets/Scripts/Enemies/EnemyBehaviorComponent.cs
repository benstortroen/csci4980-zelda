using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviorComponent : MonoBehaviour
{

    public GameObject player;
    protected Rigidbody2D rb;

    protected bool moving = false;
    protected float direction = 0;
    protected float distance = 0;
    protected Vector2 startPos;

    public float damage = 0.5f;



    public float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
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
            float new_d;
            do
            {
                new_d = UnityEngine.Random.Range(0, 8) * 90f;
            } while (new_d == direction);
            direction = new_d;

            distance = UnityEngine.Random.Range(1, 8);
            Move(direction, speed);
            moving = true;
        }
        else
        {
            float offset = Math.Abs(((Vector2)transform.position - startPos).magnitude);
            if (offset >= distance || (offset >= 2 && AlignedToPlayer() != -1))
            {
                SnapToGrid();
                moving = false;
            }
        }
    }

    protected void Move(float ang, float spd)
    {
        Vector2 vel = Quaternion.Euler(0, 0, ang) * Vector2.right;
        vel *= spd;
        rb.linearVelocity = vel;
    }

    protected void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
    }

    // Inflict damage to player
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision");
        moving = false;
        GameObject other = collision.gameObject;

        if (other.tag == "Player")
        {
            player.GetComponent<HealthComponent>().DealDamage(damage);
        }
        else if (other.tag == "Wall")
        {
            // Debug.Log("Hit wall");
        }
    }

    protected void SnapToGrid()
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

    protected int AlignedToPlayer()
    {

        float x_dif = (float)(Math.Round(player.transform.position.x) - transform.position.x);
        float y_dif = (float)(Math.Round(player.transform.position.y) - transform.position.y);
        if (Math.Abs(x_dif) < 0.1f)
        {
            return Math.Sign(y_dif) < 0 ? 3 : 1;
        }
        else if (Math.Abs(y_dif) < 0.1f)
        {
            return Math.Sign(x_dif) < 0 ? 2 : 0;
        }
        return -1;
        // if (Math.Abs(transform.position.x - Math.Round(player.transform.position.x)) < 0.1f
        //     || Math.Abs(transform.position.y - Math.Round(player.transform.position.y)) < 0.1f)
        // {
        //     return true;
        // }
        // return false;
    }
}
