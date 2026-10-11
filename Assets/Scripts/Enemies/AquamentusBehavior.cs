using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AquamentusBehavior : EnemyBehaviorComponent
{
    [SerializeField] private GameObject projectilePrefab;

    public float fireCooldown = 3f;
    private float lastFireTime = 0.0f;

    protected override void Start()
    {
        base.Start();
        distance = 1;
        direction = 180f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving && PlayerInRoom())
        {
            SnapToGrid();
            startPos = (Vector2)transform.position;
            Move(direction, speed);
            moving = true;
        }
        else if (moving)
        {
            float offset = Math.Abs(((Vector2)transform.position - startPos).magnitude);
            if (offset >= distance)
            {
                StopMovement();
                SnapToGrid();
                moving = false;
                direction = 180f + direction;
            }
        }

        if (PlayerInRoom() && Time.time - lastFireTime > fireCooldown)
        {
            Fire();
        }

    }

    private void Fire()
    {
        lastFireTime = Time.time;
        float[] directions = { 150f, 180f, 210f };
        foreach (float dir in directions)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, dir), gameObject.transform);
        }
    }
}
