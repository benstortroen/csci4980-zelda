using UnityEngine;

public interface IItem
{
    public int Damage { get; set; }

    public void UseItem(Vector3 position, Vector2 direction);

    public string ItemName { get; set; }


}
