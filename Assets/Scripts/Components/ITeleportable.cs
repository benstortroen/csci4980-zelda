using UnityEngine;
public interface ITeleportable
{

    // Indicates whether the object has been teleported 
    // to make sure it toesn't get instantly teleported back
    public bool recieved { get; set; }

    // Teleports the object to the specified position and direction
    // Different things need to behave differently when teleported.
    // e.g. arrows need to rotate, player and enemies don't,
    // enemies need to change movement pattern
    public void TeleportTo(Transform pos, Vector2 dir);
}