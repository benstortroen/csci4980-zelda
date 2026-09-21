using UnityEngine;

public class Collector : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.Log("WARNING: GameObject with Collector component is lacking an Inventory Component");
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "rupee")
        {
            Debug.Log("Collected rupee!");
            if (inventory != null)
            {
                inventory.AddRupees(1);
            }
            Destroy(other);
        }
    }
}
