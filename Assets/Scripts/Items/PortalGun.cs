using System;
using System.Collections;
using UnityEngine;

public class PortalGun : MonoBehaviour, IItem
{
    public string ItemName { get; set; }

    public int Damage { get; set; } = 0;

    private Portal portal1;
    private Portal portal2;

    [SerializeField] private GameObject portal_projectile;

    public void Start()
    {
        ItemName = "Portal Gun";
    }

    public void UseItem(Vector3 position, Vector2 direction)
    {
        // enable item
        gameObject.SetActive(true);

        // spawn arrow in facing direction
        Vector3 new_position = position + new Vector3(direction.x, direction.y, 0);
        GameObject portal = Instantiate(portal_projectile, new_position, Quaternion.identity, transform);

        portal.GetComponent<PortalProjectile>().SetDirection(direction);

        // rotate item to face dirction
        RotateItem(direction, portal);
    }

    private void RotateItem(Vector2 direction, GameObject portal)
    {
        float rotation = 0f;

        // verbose rotation control flow
        if (direction.x == 1)
        {
            rotation = 0;
        }
        else if (direction.y == 1)
        {
            rotation = 90;
        }
        else if (direction.x == -1)
        {
            rotation = 180;
        }
        else if (direction.y == -1)
        {
            rotation = 270;
        }

        portal.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void AddPortal(Portal portal)
    {
        Destroy(portal2.gameObject);
        portal2 = portal1;
        portal1 = portal;
        if (portal1 != null && portal2 != null)
        {
            portal1.LinkPortal(portal2);
            portal2.LinkPortal(portal1);
        }
    }
}
