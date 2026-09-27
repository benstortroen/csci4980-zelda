using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private int rupee_count = 0;
    private int key_count = 0;
    private float key_cooldown = 0f;
    private float key_cooldown_max = 1f;

    public void AddRupees(int num_rupees)
    {
        rupee_count += num_rupees;
    }

    public int GetRupees()
    {
        return rupee_count;
    }

    public void AddKeys(int num_keys)
    {
        key_count += num_keys;
        Debug.Log("Keys added: " + num_keys);
    }

    // Try to use key
    // If key is used, return true
    // If key cannot be used, return false
    public bool TryKey()
    {
        if (key_count > 0 && key_cooldown == 0)
        {
            key_count--;
            key_cooldown = key_cooldown_max;
            return true;
        }
        return false;
    }

    private void Update()
    {
        Debug.Log("Key count: " + key_count);

        // reduce key cooldown timer
        key_cooldown = Math.Clamp(key_cooldown - Time.deltaTime, 0, key_cooldown_max);
    }
}