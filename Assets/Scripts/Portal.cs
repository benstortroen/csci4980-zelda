using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] private Portal portal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SnapToGrid();
    }

    public void LinkPortal(Portal linkedPortal)
    {
        portal = linkedPortal;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Teleport the player to the linked portal's position
            if (portal != null)
            {
                collision.gameObject.transform.position = portal.transform.position;
            }
        }
    }

    protected void SnapToGrid()
    {
        Vector2 temp = transform.position;
        float x_offset = transform.position.x % 0.5f;
        if (x_offset < 0.25f)
        {
            temp.x -= x_offset;
        }
        else
        {
            temp.x += 0.5f - x_offset;
        }


        float y_offset = transform.position.y % 0.5f;
        if (y_offset < 0.25f)
        {
            temp.y -= y_offset;
        }
        else
        {
            temp.y += 0.5f - y_offset;
        }

        transform.position = temp;
    }
}
