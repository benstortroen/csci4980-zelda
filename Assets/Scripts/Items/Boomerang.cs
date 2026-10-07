using UnityEngine;

// throwable item
// wait until boomerang is received
// stuns enemy

public class Boomerang : MonoBehaviour, IItem
{
    // states
    public int Damage { get; set; } = 1; // should be 0.5f
    public string ItemName { get; set; }
    private bool boomerang_available;

    // prefab
    [SerializeField] private GameObject projectile_prefab;

    public void Start()
    {
        ItemName = "Boomerang";
    }

    public void UseItem(Vector3 position, Vector2 direction)
    {
        if (boomerang_available)
        {
            // enable item
            gameObject.SetActive(true);

            // spawn arrow in facing direction
            Vector3 new_position = position + new Vector3(direction.x, direction.y, 0);
            GameObject projectile = Instantiate(projectile_prefab, new_position, Quaternion.identity);

            projectile.GetComponent<Arrow>().SetDirection(direction);

            // rotate item to face dirction
            RotateItem(direction, projectile);
        }

    }

    private void RotateItem(Vector2 direction, GameObject projectile)
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

        projectile.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
