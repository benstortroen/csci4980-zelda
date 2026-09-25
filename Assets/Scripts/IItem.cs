using UnityEngine;

public interface IItem
{
    public float Damage { get; set; }
    
    public void UseItem(Vector3 position, Vector2 direction);
}
