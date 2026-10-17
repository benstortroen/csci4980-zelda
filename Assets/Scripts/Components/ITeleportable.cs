using UnityEngine;
public interface ITeleportable
{
    public bool recieved { get; set; }

    public void TeleportTo(Transform pos, Vector2 dir);
}