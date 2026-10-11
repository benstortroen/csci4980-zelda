using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // The single, globally accessible instance
    public static AudioManager Instance { get; private set; }

    // item pickups
    [SerializeField] AudioClip pickup_rupee;
    [SerializeField] AudioClip pickup_generic;

    // enemies
    [SerializeField] AudioClip enemy_hurt;
    [SerializeField] AudioClip enemy_death;

    // weapons
    [SerializeField] AudioClip item_sword;
    [SerializeField] AudioClip item_sword_projectile;
    [SerializeField] AudioClip item_arrow;

    // dungeon
    [SerializeField] AudioClip door_open;
    [SerializeField] AudioClip level_clear;
    [SerializeField] AudioClip secret;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate to enforce single instance
            return;
        }

        Instance = this;
    
        DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        
    }

    public void PlayPickupGeneric(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(pickup_generic, position);
    }

    public void PlayPickupRupee(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(pickup_rupee, position);
    }

    public void PlayEnemyDeath(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(enemy_death, position);
    }

    public void PlayEnemyHurt(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(enemy_hurt, position);
    }

    public void PlayWeaponSword(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(item_sword, position);
    }

    public void PlayWeaponSwordProjectile(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(item_sword_projectile, position);
    }

    public void PlayDoorOpen(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(door_open, position);
    }

    public void PlayLevelClear(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(level_clear, position);
    }

    public void PlaySecret(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(secret, position);
    }

    public void PlayWeaponArrow(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(item_arrow, position);
    }
}