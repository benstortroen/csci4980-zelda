using UnityEngine;

public class Arrow : MonoBehaviour, IItem
{
    public int Damage { get; set; } = 1;
    public string ItemName { get; set; } = "Arrow";

    public void UseItem(Vector3 position, Vector2 direction) { }

    [SerializeField] private float move_speed = 5f;

    private Vector2 direction;

    private void Start()
    {
        AudioManager.Instance.PlayWeaponArrow(transform.position);
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
        Debug.Log("Arrow hit something");
        if (!collision.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
