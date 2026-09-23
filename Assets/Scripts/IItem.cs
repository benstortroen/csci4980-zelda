using UnityEngine;

public class Item
{
    private float name;
    private float damage;
    private Vector2 direction;
    
    public void UseItem()
    {
        
    }
}

public class ProjectileItem : Item
{
    [SerializeField] GameObject projectileGameObject;


}

public class Projectile
{
    private float speed = 1;
    private Vector2 direction = Vector2.right;

    public Projectile(Vector2 dir)
    {
        direction = dir;
    }

    void Update()
    {
        
    }
}
