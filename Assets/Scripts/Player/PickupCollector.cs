using UnityEngine;

public class PickupCollector : MonoBehaviour
{
    private Inventory inventory;
    private ItemManager itemManager;
    private HealthComponent healthComponent;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.Log("WARNING: GameObject with Collector component is lacking an Inventory Component");
        }
        itemManager = GetComponent<ItemManager>();
        healthComponent = GetComponent<HealthComponent>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        // Collect Rupee
        if (other.tag == "rupee")
        {
            Debug.Log("Collected rupee!");
            if (inventory != null)
            {
                inventory.AddRupees(1);
            }
            Destroy(other);

            // Play collect noise
            AudioManager.Instance.PlayPickupRupee(transform.position);
        }

        if (other.tag == "heart")
        {
            healthComponent.RestoreHealth(1);
            Destroy(other);
            AudioManager.Instance.PlayPickupGeneric(transform.position);
        }

        if (other.tag == "key")
        {
            if (inventory != null)
            {
                inventory.AddKeys(1);
                AudioManager.Instance.PlayPickupGeneric(transform.position);
            }
            Destroy(other);
        }

        if (other.tag == "pickup")
        {
            itemManager.AddItem(other);
            AudioManager.Instance.PlayPickupGeneric(transform.position);
        }
    }
}
