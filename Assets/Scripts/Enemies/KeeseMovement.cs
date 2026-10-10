using System;
using Unity.VisualScripting;
using UnityEngine;

public class KeeseMovement : EnemyBehaviorComponent
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving && PlayerInRoom())
        {
            // Debug.Log("Moving");
            SnapToGrid();
            startPos = (Vector2)transform.position;
            float new_d;
            do
            {
                new_d = UnityEngine.Random.Range(0, 8) * 45f;
            } while (new_d == -direction);
            direction = new_d;
            if (new_d % 90 != 0)
            {
                float unit = (float)Math.Sqrt(2f);
                distance = UnityEngine.Random.Range(0, 9) * unit;
            }
            else
            {
                distance = UnityEngine.Random.Range(1, 8);
            }
            Move(direction, speed);
            moving = true;
        }
        else
        {
            float offset = Math.Abs(((Vector2)transform.position - startPos).magnitude);
            if (offset >= distance || (offset >= 5 && AlignedToPlayer() != -1))
            {
                SnapToGrid();
                moving = false;
            }
        }
    }

}



