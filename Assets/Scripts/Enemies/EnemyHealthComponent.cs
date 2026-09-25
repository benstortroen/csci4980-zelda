using UnityEngine;

public class EnemyHealthComponent : HealthComponent
{
    public override void OnDeath()
    {
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
