using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class Sword : MonoBehaviour, IItem
{
    
    public int Damage { get; set; } = 1; 
    
    public void UseItem(Vector3 position, Vector2 direction)
    {
        // enable item
        gameObject.SetActive(true);

        // rotate item to face dirction
        RotateItem(direction);
        
        // place sword in facing direction 
        transform.position = position + new Vector3(direction.x, direction.y, 0);

        // disable item
        StartCoroutine("DisableItem");
    }

    IEnumerator DisableItem()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

    private void RotateItem(Vector2 direction)
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

        transform.rotation = Quaternion.Euler(0, 0 , rotation);
    }

}
