using UnityEngine;

public class SpikeTrapMovement : EnemyMovement
{

    private bool resetting = false;
    public float resetSpeed = 2f;
    private Vector2 sp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        speed = 3;
        sp = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!resetting)
        {
            if (!moving)
            {
                int alignment = AlignedToPlayer();
                if (alignment != -1)
                {
                    direction = alignment * 90f;
                    MoveDirSpd(alignment * 90f, speed);
                }
                moving = true;
            }
        }
        else if (!moving)
        {

        }
    }
}
