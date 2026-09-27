using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    private Inventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }


    //Locked door is literally just a box collider square
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        GameObject other = collision.gameObject;

        if (other.tag == "locked_door" && inventory.GetKeys() > 0)
        {
            inventory.UseKey();
            LockedDoor lockedDoor = other.GetComponent<LockedDoor>();
            if (lockedDoor != null)
            {
                lockedDoor.Unlock();
            }
        }
    }
}
