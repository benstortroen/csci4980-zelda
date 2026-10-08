using Unity.VisualScripting;
using UnityEngine;

public class BoomerangProjectile : MonoBehaviour, IItem
{
    // stats
    public int Damage { get; set; } = 1;
    public string ItemName { get; set; } = "Boomerange";
    [SerializeField] private float move_speed = 5.0f;
    private Vector2 direction;

    // player
    private Transform player_transform;
    public Boomerang boomerang;

    // track distance thrown
    private Vector3 initPos;
    private float maxThrowDistance = 4.0f;
    private bool tracking_player = false;

    public void UseItem(Vector3 position, Vector2 direction) { }

    private void Start()
    {
        initPos = transform.position;
        player_transform = GameObject.FindWithTag("Player").transform;
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
            Vector3 towards_player = (player_transform.position - transform.position).normalized;
            SetDirection(towards_player);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // boomerang returns to player
            if (tracking_player)
            {
                Debug.Log("Boomerang returned to player");
                boomerang.boomerang_available = true;
                gameObject.SetActive(false);
            }
        }

        // boomerang hits enemy or wall
        // go back to player
        else if (!collision.isTrigger)
        {
            tracking_player = true;

            // TODO: stun enemy on hit
            // check collision tag for enemy
            // set moving to false
            // start coroutine to set moving to true after 2 seconds
        }

        
    }
}
