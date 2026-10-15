using UnityEngine;

public class PortalProjectile : MonoBehaviour, IItem
{
    public int Damage { get; set; } = 0;
    public string ItemName { get; set; } = "Portal Projectile";

    public void UseItem(Vector3 position, Vector2 direction) { }

    public Portal PortalPrefab;

    [SerializeField] private float move_speed = 5f;

    private Vector2 direction;

    private void Start()
    {
    }

    private void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * move_speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Not colliding with enemy or player summon portal
        if (collision.tag != "Player" && collision.tag != "Enemy")
        {
            Portal portal = Instantiate(PortalPrefab, transform.position, Quaternion.identity);
            transform.parent.gameObject.GetComponent<PortalGun>().AddPortal(portal);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
