using UnityEngine;

public class Collector : MonoBehaviour
{
    private Inventory inventory;
    private HealthComponent healthComponent;
    public AudioClip rupee_collection_sound_clip;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.Log("WARNING: GameObject with Collector component is lacking an Inventory Component");
        }

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
            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, transform.position);
        }

        if (other.tag == "heart")
        {
            healthComponent.RestoreHealth(1);
            Destroy(other);
        }

        if (other.tag == "key")
        {
            if (inventory != null)
            {
                inventory.AddKeys(1);
            }
            Destroy(other);
        }
    }
}
