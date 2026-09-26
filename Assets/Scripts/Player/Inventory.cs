using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private int rupee_count = 0;
    private int key_count = 0;

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

    public int GetKeys()
    {
        return key_count;
    }

    public void UseKey()
    {
        if (key_count > 0)
        {
            Debug.Log("Key used.");
            key_count--;
        }
    }
}