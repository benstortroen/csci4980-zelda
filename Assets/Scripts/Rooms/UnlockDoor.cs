using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "locked_door" )
        {
            Debug.Log("Player collided with locked door");
            
            if (inventory.GetKeys() > 0 || CheatsController.godMode)
            {
                Debug.Log("Unlocking door...");
                // Attempt to unlock door
                inventory.UseKey();
                LockedDoor lockedDoor = other.GetComponent<LockedDoor>();
                if (lockedDoor != null)
                {
                    lockedDoor.Unlock();
                }
            }
            
        }
    }
}
