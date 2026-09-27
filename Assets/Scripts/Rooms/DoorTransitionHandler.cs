using UnityEngine;
using UnityEngine.Tilemaps;

// This door handler will be placed on the player
// It sends a message to the camera to move into a new room 
public class DoorTransitionHandler : MonoBehaviour
{
    [SerializeField] MessengerComponent messenger;

    // send direction to move, depending on tile position
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "door")
        {
            // Tile localPosition is relative to room (0-16, 0-11)
            Transform tileTransform = collision.transform;
            Vector3 tilePos = tileTransform.localPosition; 

            Debug.Log("Door tile localPosition: " + tilePos);

            // Right Door 
            if (tilePos.x == 14)
            {
                Debug.Log("Entered right door");
                messenger.send(new ArrowInputMessage(this, TransitionMessage.RIGHT_ARROW));
            }
            // Left Door 
            else if (tilePos.x == 1)
            {
                Debug.Log("Entered left door");
                messenger.send(new ArrowInputMessage(this, TransitionMessage.LEFT_ARROW));
            }

            // Up Door
            else if (tilePos.y == 9)
            {
                Debug.Log("Entered up door");
                messenger.send(new ArrowInputMessage(this, TransitionMessage.UP_ARROW));
            }

            // Down Door
            else if (tilePos.y == 1)
            {
                Debug.Log("Entered down door");
                messenger.send(new ArrowInputMessage(this, TransitionMessage.DOWN_ARROW));
            }
        }
    }
}
