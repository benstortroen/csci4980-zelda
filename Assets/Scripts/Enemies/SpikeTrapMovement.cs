using UnityEngine;
using System;

public class SpikeTrapMovement : EnemyBehaviorComponent
{

    private enum State
    {
        Moving,
        Resetting,
        Idle
    };

    [SerializeField] Transform roomPos;

    private State state = State.Idle;
    public float resetSpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (state == State.Idle)
        {
            StopMovement();
            if (PlayerInRoom())
            {
                int alignment = AlignedToPlayer();
                // Debug.Log("Idle");
                if (alignment != -1)
                {
                    direction = alignment * 90f;
                    Move(direction, speed);
                    state = State.Moving;
                }
            }

        }
        else if (state == State.Resetting)
        {
            // Debug.Log("Resetting");
            float offset = Math.Abs(((Vector2)transform.position - startPos).magnitude);
            Move(direction, resetSpeed);
            if (offset < 0.1f)
            {
                state = State.Idle;
                StopMovement();
            }
        }
        else
        {
            // Debug.Log("Moving");
            Move(direction, speed);
        }
    }



    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            player.GetComponent<HealthComponent>().DealDamage(damage);
        }
        else if (state == State.Moving)
        {
            StopMovement();
            SnapToGrid();
            StartReset();
        }
        else
        {
            SnapToGrid();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            StopMovement();
            SnapToGrid();
            StartReset();
        }
    }


    private void StartReset()
    {
        state = State.Resetting;
        direction = direction + 180f;
        Move(direction, resetSpeed);
    }
}

