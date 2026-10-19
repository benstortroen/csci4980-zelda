using UnityEngine;

public class Arrow : MonoBehaviour, IItem, ITeleportable
{
    public int Damage { get; set; } = 1;
    public string ItemName { get; set; } = "Arrow";

    public bool recieved { get; set; } = false;

    public void UseItem(Vector3 position, Vector2 direction) { }

    [SerializeField] private float move_speed = 5f;


    public float max_time = 5;
    private float spawn_time;

    private Vector2 direction;

    private void Start()
    {
        spawn_time = Time.time;
        AudioManager.Instance.PlayWeaponArrow(transform.position);
    }

    private void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * move_speed * Time.deltaTime;
        if (Time.time - spawn_time > max_time)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;

        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, dir));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }

    public void TeleportTo(Transform pos, Vector2 dir)
    {
        SetDirection(dir);
        transform.position = pos.position;
    }
}
