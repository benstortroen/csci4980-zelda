using UnityEngine;

public class Portal : MonoBehaviour
{

    // Reciever portal
    [SerializeField] Portal partnerPortal;

    //The unit vector of the direction the portal is facing
    Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SnapToGrid();
        SnapRotation();
        direction = transform.rotation * Vector2.right;
    }


    private void SnapToGrid()
    {
        Vector2 temp = transform.position;
        float x_offset = transform.position.x % 1f;
        if (x_offset < 0.5f)
        {
            temp.x -= x_offset;
        }
        else
        {
            temp.x += 1f - x_offset;
        }


        float y_offset = transform.position.y % 1f;
        if (y_offset < .5f)
        {
            temp.y -= y_offset;
        }
        else
        {
            temp.y += 1f - y_offset;
        }

        transform.position = temp;
    }

    public void SnapRotation()
    {
        float snappedZ = Mathf.Round(transform.eulerAngles.z / 90f) * 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, snappedZ);
    }


    //All logic will be done on the recieving end or inside the
    // teleported object itself
    public void Recieve(ITeleportable obj)
    {
        obj.TeleportTo(transform, direction);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ITeleportable>(out var obj) && !obj.recieved)
        {
            partnerPortal.Recieve(obj);
        }
    }
}
