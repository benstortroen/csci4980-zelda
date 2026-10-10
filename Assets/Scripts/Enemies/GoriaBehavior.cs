using System;
using Unity.VisualScripting;
using UnityEngine;

public class GoriaBehavior : EnemyBehaviorComponent
{
    [SerializeField] private GameObject boomerangPrefab;
    public bool hasBoomerang = true;

    // Update is called once per frame
    void Update()
    {
        if (!moving && PlayerInRoom() && hasBoomerang)
        {
            SnapToGrid();
            startPos = (Vector2)transform.position;
            float new_d;
            do
            {
                new_d = UnityEngine.Random.Range(0, 8) * 90f;
            } while (new_d == -direction);
            direction = new_d;

            distance = UnityEngine.Random.Range(1, 8);
            Move(direction, speed);
            moving = true;
        }
        else if (moving)
        {
            float offset = Math.Abs(((Vector2)transform.position - startPos).magnitude);
            if (offset >= distance || (offset >= 2 && AlignedToPlayer() != -1))
            {
                StopMovement();
                SnapToGrid();
                moving = false;
                ThrowBoomerang();
            }
        }
    }

    private void ThrowBoomerang()
    {
        hasBoomerang = false;
        Vector2 new_position = transform.position + (Quaternion.Euler(0, 0, direction) * Vector2.right);
        EvilBoomerangProjectile evilBoomerangProjectile = Instantiate(boomerangPrefab, new_position, Quaternion.identity, gameObject.transform).GetComponent<EvilBoomerangProjectile>();
        evilBoomerangProjectile.SetOwner(gameObject);
        evilBoomerangProjectile.SetDirection(Quaternion.Euler(0, 0, direction) * Vector2.right);
    }
}
