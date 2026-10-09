using UnityEngine;

public class Bomb : MonoBehaviour, IItem

{
    public int Damage { get; set; } = 4;
    public string ItemName { get; set; } = "Bomb";

    [SerializeField] GameObject bomb_prefab;

    public void UseItem(Vector3 position, Vector2 direction)
    {
        // enable item
        gameObject.SetActive(true);

        // spawn arrow in facing direction
        Vector3 new_position = position + new Vector3(direction.x, direction.y, 0);
        GameObject bomb = Instantiate(bomb_prefab, new_position, Quaternion.identity);


    }
}
