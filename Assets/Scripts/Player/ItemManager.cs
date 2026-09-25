using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    // Items
    [SerializeField] private GameObject main_item_prefab;
    [SerializeField] private GameObject[] alt_items_prefab;
    private GameObject main_item;
    private GameObject alt_item;
    private GameObject[] alt_items;
    private int alt_index;
    

    // Components
    private ArrowKeyMovement arrowKeyMovement;
    private Vector2 facing_direction;

    void Start()
    {
        arrowKeyMovement = gameObject.GetComponent<ArrowKeyMovement>();

        // Instantiate main item
        main_item = Instantiate(main_item_prefab, Vector3.zero, Quaternion.identity);
        main_item.SetActive(true);
        // Instantiate alt items    
        alt_items = new GameObject[alt_items_prefab.Length];
        for (int i = 0; i < alt_items_prefab.Length; i++)
        {
            alt_items[i] = Instantiate(alt_items_prefab[i], Vector3.zero, Quaternion.identity);
        }

        alt_item = alt_items[0];
        alt_item.SetActive(true);
    }

    // use item when key is pressed
    
    void Update()
    {
        
        UpdateDirection();
        HandleInput();
    }

    void UpdateDirection()
    {
        facing_direction = arrowKeyMovement.GetDirectionFacing();
    }

    // TODO: Add cooldown between item use?
    void HandleInput()
    {
        // Use main item on "X" keystroke
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            Debug.Log("Used main item");
            main_item.GetComponent<IItem>().UseItem(transform.position, facing_direction);
        }

        // Use alt item on "Z" keystroke
        else if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            Debug.Log("Used alt item");
            alt_item.GetComponent<IItem>().UseItem(transform.position, facing_direction);
        }
        // Cycle alt item on "Space" keystroke
        else if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Swapped alt item");
            alt_index++;
            if (alt_index >= alt_items.Length) alt_index = 0;
            // Swap alt item
            alt_item.SetActive(false);
            alt_item = alt_items[alt_index];
            alt_item.SetActive(true);
        }
    }

}


// ItemManager
// Player has list of items in arsenal
// Player is facing a direction
// [ ] "x" for standard weapon. "z" for alt weapon.
// [ ] "SPACE BAR" to toggle between / scroll through alt weapons. 
// Do not use a menu. 
// Spacebar is pressed, and the next available secondary weapon is selected (checked during p1_alpha)


// IItem
// Items have a name
// Items have a position on the sprite 
// Items have a collision
// Items have an effect on collision
// Some items shoot a projectile
// Some items have a damage
// Some items cost rupees to use


// List of Items
// Sword
// Bow
// Boomerang
// Bomb