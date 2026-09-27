using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Turn off door collision when locked
public class LockedDoor : MonoBehaviour
{
    private bool is_locked = true;
    private BoxCollider2D door_collider;
    [SerializeField] GameObject doorPrefab;

    // Create a mapping of locked door sprites to open door sprites
    [SerializeField] private Sprite lockedDoorRight;
    [SerializeField] private Sprite lockedDoorLeft; 
    [SerializeField] private Sprite lockedDoorUp1;
    [SerializeField] private Sprite lockedDoorUp2;
    [SerializeField] private Sprite lockedDoorDown1;
    [SerializeField] private Sprite lockedDoorDown2;
    [SerializeField] private Sprite openDoorRight;
    [SerializeField] private Sprite openDoorLeft; 
    [SerializeField] private Sprite openDoorUp1;
    [SerializeField] private Sprite openDoorUp2;
    [SerializeField] private Sprite openDoorDown1;
    [SerializeField] private Sprite openDoorDown2;
    Dictionary<Sprite, Sprite> spriteMap = new Dictionary<Sprite, Sprite>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // door_collider = GetComponent<BoxCollider2D>();
        spriteMap.Add(lockedDoorRight, openDoorRight);
        spriteMap.Add(lockedDoorLeft, openDoorLeft);
        spriteMap.Add(lockedDoorUp1, openDoorUp1);
        spriteMap.Add(lockedDoorUp2, openDoorUp2);
        // I don't think there are locked doors for the down direction
        // spriteMap.Add(lockedDoorDown1, openDoorDown1);
        // spriteMap.Add(lockedDoorDown2, openDoorDown2);
    }

    public bool IsLocked()
    {
        return is_locked;
    }

    public void Unlock()
    {
        Debug.Log("Door unlocked!");
        // is_locked = false;
        // door_collider.enabled = false;
        ReplaceTile();
    }

    // Change LOCK tile with DOOR tile 
    private void ReplaceTile()
    {
        // TODO: Change prefab to Tile_DOOR
        GameObject door = Instantiate(doorPrefab, gameObject.transform);
        door.transform.parent = gameObject.transform.parent;

        // TODO: Change sprite to open door
        // Make a dictionary for door sprite mappings
        SpriteRenderer lockSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer doorSpriteRenderer = door.GetComponent<SpriteRenderer>();
        doorSpriteRenderer.sprite = spriteMap[lockSpriteRenderer.sprite];

        Destroy(gameObject);
    }

    
}
