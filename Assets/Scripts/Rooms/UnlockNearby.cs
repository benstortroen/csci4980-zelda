using UnityEngine;

// Unlocks nearby locked door tiles
// This dummy gameobject is spawned when a door is opened
// It is meant to unlock 2 tile wide doors

public class UnlockNearby : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "locked_door")
        {
            LockedDoor lockedDoor = collision.gameObject.GetComponent<LockedDoor>();
            lockedDoor.Unlock();
        }
    }
}
