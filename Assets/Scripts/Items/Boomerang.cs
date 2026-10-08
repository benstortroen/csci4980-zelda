using UnityEngine;

// throwable item
// goes 5 blocks
// takes directional input 
// wait until boomerang is received
// stuns enemy

public class Boomerang : MonoBehaviour, IItem
{
    // states
    public int Damage { get; set; } = 1; // should be 0.5f
    public string ItemName { get; set; }
    public bool boomerang_available;

    // prefab
    [SerializeField] private GameObject projectile_prefab;

    public void Start()
    {
        ItemName = "Boomerang";
        boomerang_available = true;
    }

    public void UseItem(Vector3 position, Vector2 direction)
    {
        
        if (boomerang_available)
        {
            Debug.Log("using boomering");

            // enable item
            gameObject.SetActive(true);

            // prevent boomerang from being recasted
            boomerang_available = false;

            // create direction vector from input
            float horizontal_input = Input.GetAxisRaw("Horizontal");
            float vertical_input = Input.GetAxisRaw("Vertical");
            Vector2 input_dir = new Vector3(horizontal_input, vertical_input);
            input_dir.Normalize();

            // use input_dir only if it is non-zero
            if (input_dir != Vector2.zero)
            {
                direction = input_dir;
            }

            // spawn projectile
            Vector3 new_position = position + new Vector3(direction.x, direction.y, 0);
            GameObject projectile = Instantiate(projectile_prefab, new_position, Quaternion.identity);

            // set projectile parameters
            BoomerangProjectile boomerang_projectile = projectile.GetComponent<BoomerangProjectile>();
            boomerang_projectile.SetDirection(direction);
            boomerang_projectile.boomerang = gameObject.GetComponent<Boomerang>();
        }

    }
}
