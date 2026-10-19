using System.Collections;
using UnityEngine;

public class SwordProjecile : MonoBehaviour, IItem
{
    public int Damage { get; set; } = 1;
    public string ItemName { get; set; } = "SwordProjectile";
    private bool hasHit = false;

    public void UseItem(Vector3 position, Vector2 direction) { }

    [SerializeField] private float move_speed = 10f;

    private Vector2 direction;

    private void Update()
    {
       if (!hasHit) {
        transform.position += new Vector3(direction.x, direction.y, 0) * move_speed * Time.deltaTime;
       }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.tag != "Player")
        {
            // play sword animations
            hasHit = true;
            AudioManager.Instance.PlayWeaponSwordProjectile(transform.position);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Animator>().Play("sword_projectile_hit");
            StartCoroutine(DestroyOnFin());
            
        }
        
    }

    // destroy object when projectile hit animation finishes 
    IEnumerator DestroyOnFin()
    {
        yield return null;
        yield return new WaitForSeconds(0.24f);
        Destroy(gameObject);
    }
}
