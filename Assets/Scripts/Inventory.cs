using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private int rupee_count = 0;

    public void AddRupees(int num_rupees)
    {
        rupee_count += num_rupees;
    }

    public int GetRupees()
    {
        return rupee_count;
    }
}

public class ItemManager : MonoBehaviour
{
    private GameObject main_item;
    private GameObject alt_item;
    private int alt_index;
    [SerializeField] private GameObject[] items;
    


    private Vector2 facing_direction;

    // use item when key is pressed
    // TODO: Add cooldown between item use?
    void Update()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            // Use main item on "X" keystroke
            main_item.GetComponent<Item>().UseItem();
        }

        else if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            // Use alt item on "Z" keystroke
            alt_item.GetComponent<Item>().UseItem();
        }
        else if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Cycle alt item on "Space" keystroke
            alt_index++;
            if (alt_index >= items.Length) alt_index = 0;
            SwapAltItem(items[alt_index]);
        }
    }

    // Disable old alt_item, enable new alt time
    private void SwapAltItem(GameObject new_item)
    {
        alt_item.SetActive(false);
        alt_item = new_item;
        alt_item.SetActive(true);
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