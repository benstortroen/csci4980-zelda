using UnityEngine;

public class EnemyHealthComponent : HealthComponent
{

    [SerializeField] private GameObject[] drops;
    [SerializeField] private GameObject uniqueDrop = null;
    private int dropCount;
    protected override void Start()
    {
        base.Start();
        dropCount = drops.Length;
    }
    public override void OnDeath()
    {
        if (uniqueDrop != null)
        {
            Instantiate(uniqueDrop, transform.position, Quaternion.identity);
        }
        else
        {
            int drop = Random.Range(0, 2 * dropCount);
            if (drop < dropCount)
            {
                Instantiate(drops[drop], transform.position, Quaternion.identity);
            }
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
            gameObject.GetComponent<EnemyBehaviorComponent>().Knockback();
        }
    }
}
