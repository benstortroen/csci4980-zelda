using UnityEngine;

public class EnemyHealthComponent : HealthComponent
{

    [SerializeField] private GameObject rupee;
    [SerializeField] private GameObject heart;
    void Start()
    {
        max_hp = 3f;
        current_hp = max_hp;
    }
    public override void OnDeath()
    {

        int drop = Random.Range(0, 5);
        if (drop == 0)
        {
            Instantiate(rupee, transform.position, Quaternion.identity);
        }
        else if (drop == 1)
        {
            Instantiate(heart, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }

    // Take damage from item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "item")
        {
            // get item reference
            IItem item = collision.GetComponent<IItem>();

            // deal damage to enemy (self)
            DealDamage(item.Damage);
        }
    }
}
