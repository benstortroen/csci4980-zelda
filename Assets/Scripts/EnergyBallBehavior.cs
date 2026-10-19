using UnityEngine;

public class EnergyBallBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private float damage = 0.5f;


    void Update()
    {
        Vector3 direction = transform.rotation * Vector2.right;
        gameObject.transform.position += speed * direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Energy ball hit something!");
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<HealthComponent>().IsInvulnerable())
        {
            GameObject player = collision.gameObject;
            player.GetComponent<HealthComponent>().DealDamage(damage);
            player.GetComponent<ArrowKeyMovement>().Knockback((Vector2)player.transform.position - (Vector2)transform.position);
        }
        gameObject.SetActive(false);
    }
}