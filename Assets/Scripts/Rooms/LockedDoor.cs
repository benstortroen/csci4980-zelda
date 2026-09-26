using System;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private bool is_locked = true;
    private BoxCollider2D door_collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        door_collider = GetComponent<BoxCollider2D>();
    }

    public void Unlock()
    {
        is_locked = false;
        door_collider.enabled = false;
        Debug.Log("Door unlocked!");
    }

    public bool IsLocked()
    {
        return is_locked;
    }
}
