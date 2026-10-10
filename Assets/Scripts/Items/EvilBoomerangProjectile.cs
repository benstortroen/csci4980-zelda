using Unity.VisualScripting;
using UnityEngine;

public class EvilBoomerangProjectile : MonoBehaviour
{
    // stats
    public int Damage = 1;
    [SerializeField] private float move_speed = 5.0f;
    private Vector2 direction;

    private GameObject owner;

    // track distance thrown
    private Vector3 initPos;
    private float maxThrowDistance = 4.0f;
    private bool tracking_target = false;

    public void UseItem(Vector3 position, Vector2 direction) { }

    private void Start()
    {
        initPos = transform.position;
    }

    public void SetOwner(GameObject target)
    {
        owner = target;
    }

    private void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * move_speed * Time.deltaTime;

        // if boomerang has traveled beyond the max throw distance
        if ((transform.position - initPos).magnitude > maxThrowDistance)
        {
            tracking_target = true;
        }

        // move boomerang back to target
        if (tracking_target)
        {
            Vector3 towards_target = (owner.transform.position - transform.position).normalized;
            SetDirection(towards_target);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner)
        {
            // boomerang returns to goria
            if (tracking_target)
            {
                Debug.Log("Boomerang returned to target");
                owner.GetComponent<GoriaBehavior>().hasBoomerang = true;
                gameObject.SetActive(false);
            }
        }

        // boomerang hits enemy
        // go back to player
        else if (collision.tag == "Player")
        {
            tracking_target = true;
            collision.gameObject.GetComponent<PlayerHealthComponent>().DealDamage(Damage);

            // TODO: stun enemy on hit
            // check collision tag for enemy
            // set moving to false
            // start coroutine to set moving to true after 2 seconds
        }

        else if (collision.CompareTag("Wall"))
        {
            tracking_target = true;
        }


    }
}
