using UnityEngine;

public class BoomerangProjectile : MonoBehaviour
{
    // stats
    private float damage = 0.5f;
    [SerializeField] private float move_speed = 3f;
    private Vector2 direction;

    // player
    [SerializeField] Transform player_transform;
    [SerializeField] Boomerang boomerang;

    // track distance thrown
    private Vector3 initPos;
    private float maxThrowDistance = 5.0f;
    private bool tracking_player = false;

    private void Start()
    {
        initPos = transform.position;;
    }
    
    private void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * move_speed * Time.deltaTime;

        // if boomerang has traveled beyond the max throw distance
        if ((transform.position - initPos).magnitude > maxThrowDistance)
        {
            tracking_player = true;
        }

        // move boomerang back to player
        if (tracking_player)
        {
            SetDirection(player_transform.position - transform.position);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // boomerang returns to player
        if (tracking_player && collision.gameObject.tag == "player")
        {
            Debug.Log("Boomerang returned to player");
            boomerang.boomerang_available = true;
            gameObject.SetActive(false);
        }

        // boomerang hits enemy or wall
        // go back to player
        else
        {
            tracking_player = true;
        }
        
    }
}
