using UnityEngine;

public class WallMasterBehavior : EnemyBehaviorComponent
{

    [SerializeField] private Transform cameraTransform;
    private Vector2 playerStartLoc;
    private Vector3 cameraStartLoc;

    protected override void Start()
    {
        base.Start();
        playerStartLoc = player.transform.position;
        cameraStartLoc = cameraTransform.position;

    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision");
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = playerStartLoc;
            cameraTransform.position = cameraStartLoc;
        }


    }
}
