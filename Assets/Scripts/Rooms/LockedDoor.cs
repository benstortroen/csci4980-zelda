using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Turn off door collision when locked
public class LockedDoor : MonoBehaviour
{
    private bool is_locked = true;
    [SerializeField] GameObject doorPrefab;
 
    // Create a mapping of locked door sprites to open door sprites
    [SerializeField] private Sprite lockedDoorRight;
    [SerializeField] private Sprite lockedDoorLeft; 
    [SerializeField] private Sprite lockedDoorUp1;
    [SerializeField] private Sprite lockedDoorUp2;
    [SerializeField] private Sprite openDoorRight;
    [SerializeField] private Sprite openDoorLeft; 
    [SerializeField] private Sprite openDoorUp1;
    [SerializeField] private Sprite openDoorUp2;
    Dictionary<Sprite, Sprite> spriteMap = new Dictionary<Sprite, Sprite>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteMap.Add(lockedDoorRight, openDoorRight);
        spriteMap.Add(lockedDoorLeft, openDoorLeft);
        spriteMap.Add(lockedDoorUp1, openDoorUp1);
        spriteMap.Add(lockedDoorUp2, openDoorUp2);
        // I don't think there are locked doors for the down direction
    }

    public bool IsLocked()
    {
        return is_locked;
    }

    public void Unlock()
    {
        if (!is_locked) return;
        is_locked = false;

        Debug.Log("Door unlocked!");
        UnlockNearby();
        ReplaceTile();
    }

    // Unlock sibling tiles if they are locked doors
    private void UnlockNearby()
    {
        // Unlock left sibling
        int index = transform.GetSiblingIndex(); 
        GameObject leftSibling = transform.parent.GetChild(index - 1).gameObject; 
        if (leftSibling.tag == "locked_door")
        {
            leftSibling.GetComponent<LockedDoor>().Unlock();
        }
        
        // Unlock right sibling
        int siblingCount = transform.parent.childCount;
        // Guard for going over child indicies
        if (index < siblingCount - 1)
        {
            GameObject rightSibling = transform.parent.GetChild(index + 1).gameObject; 
            if (rightSibling.tag == "locked_door")
            {
                rightSibling.GetComponent<LockedDoor>().Unlock();
            }
        }
    }
    
    // Change LOCK tile with DOOR tile 
    private void ReplaceTile()
    {
        // Change prefab to Tile_DOOR
        GameObject door = Instantiate(doorPrefab, transform);
        door.transform.parent = gameObject.transform.parent;

        // Change sprite to open door
        // Use dictionary for door sprite mappings
        SpriteRenderer lockSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer doorSpriteRenderer = door.GetComponent<SpriteRenderer>();
        doorSpriteRenderer.sprite = spriteMap[lockSpriteRenderer.sprite];

        Destroy(gameObject);
    }

    
}
