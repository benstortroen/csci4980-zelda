using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GoriaBehavior : EnemyBehaviorComponent
{
    [SerializeField] private GameObject boomerangPrefab;
    public bool hasBoomerang = true;

    public float throwDelay = 1f;

    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite upSprite;
    [SerializeField] Sprite downSprite;
    [SerializeField] Sprite rightSprite;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving && PlayerInRoom() && hasBoomerang)
        {
            Debug.Log("Starting Movement");
            SnapToGrid();
            startPos = (Vector2)transform.position;
            float new_d;
            do
            {
                new_d = UnityEngine.Random.Range(0, directions) * (360f / (float)directions);
            } while (new_d == -direction);

            direction = new_d;
            SetSprite(direction);

            distance = UnityEngine.Random.Range(minDistance, maxDistance);
            Move(direction, speed);
            moving = true;
        }
        else if (moving && hasBoomerang)
        {
            float offset = Math.Abs(((Vector2)transform.position - startPos).magnitude);
            if (offset >= distance || (offset >= 2 && AlignedToPlayer() != -1))
            {
                StopMovement();
                SnapToGrid();
                moving = false;
                StartCoroutine(ThrowBoomerangDelayed());
            }
        }
    }

    private void SetSprite(float dir)
    {
        switch (dir)
        {
            case 0:
                spriteRenderer.flipX = false;
                spriteRenderer.sprite = rightSprite;
                break;
            case 90:
                spriteRenderer.sprite = upSprite;
                break;
            case 180:
                spriteRenderer.flipX = true;
                spriteRenderer.sprite = rightSprite;
                break;
            default:
                spriteRenderer.sprite = downSprite;
                break;
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

    private IEnumerator ThrowBoomerangDelayed()
    {
        hasBoomerang = false;
        float throwDirection;
        do
        {
            throwDirection = UnityEngine.Random.Range(0, directions) * (360f / (float)directions);
        } while (throwDirection == -direction);
        direction = throwDirection;
        SetSprite(direction);
        yield return new WaitForSeconds(throwDelay);
        ThrowBoomerang();
    }
}
