using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class Sword : MonoBehaviour, IItem
{
    public string ItemName { get; set; } = "Sword";
    public int Damage { get; set; } = 1;

    [SerializeField] private GameObject sword_prefab;
    private float projectileCooldown = 1.0f;
    private float projectileLastUsed = 0.0f;

    public void UseItem(Vector3 position, Vector2 direction)
    {
        gameObject.SetActive(false);
        AudioManager.Instance.PlayWeaponSword(transform.position);

        // Spawn Sword hitbox
        // rotate item to face dirction
        RotateItem(direction);
        // place sword in facing direction 
        Vector3 new_position = position + new Vector3(direction.x, direction.y, 0);
        transform.position = new_position;
        // disable item

        // Check if player is max health so projectile should be spawned
        GameObject player = GameObject.FindWithTag("Player");
        HealthComponent healthComponent = player.GetComponent<PlayerHealthComponent>();
        // TODO: edit cooldown to be controlled by existance of projectile
        bool offCooldown = (Time.time - projectileLastUsed) > projectileCooldown;
        if (healthComponent.IsFull() && offCooldown)
        {
            // Spawn SwordProjectile
            // TODO: Add delay between 
            GameObject projectile = Instantiate(sword_prefab, new_position, transform.rotation);
            projectile.GetComponent<SwordProjecile>().SetDirection(direction);
            projectileLastUsed = Time.time;
        }
        else
        {
            gameObject.SetActive(true);
            StartCoroutine("DisableItem");
        }
    }

    IEnumerator DisableItem()
    {
        yield return new WaitForSeconds(0.25f);
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

        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

}
