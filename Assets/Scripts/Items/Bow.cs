using System;
using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour, IItem
{
    public int Damage { get; set; } = 1; 

    [SerializeField] private GameObject arrow_prefab;
    
    public void UseItem(Vector3 position, Vector2 direction)
    {
        // enable item
        gameObject.SetActive(true);

        // spawn arrow in facing direction
        Vector3 new_position = position + new Vector3(direction.x, direction.y, 0);
        GameObject arrow = Instantiate(arrow_prefab, new_position, Quaternion.identity);

        arrow.GetComponent<Arrow>().SetDirection(direction);

        // rotate item to face dirction
        RotateItem(direction, arrow);
        
        
    }

    private void RotateItem(Vector2 direction, GameObject arrow)
    {
        float rotation = 0f;

        // verbose rotation control flow
        if (direction.x == 1)
        {
            rotation = 0;
        }
        else if (direction.y == 1)
        {
            rotation = 90;
        }
        else if (direction.x == -1)
        {
            rotation = 180;
        }
        else if (direction.y == -1)
        {
            rotation = 270;
        }

        arrow.transform.rotation = Quaternion.Euler(0, 0 , rotation);
    }

}
