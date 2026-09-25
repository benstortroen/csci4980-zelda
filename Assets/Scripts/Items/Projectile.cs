using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private Vector3 direction;

    public Projectile(Vector3 dir)
    {
        direction = dir;
    }

    void Update()
    {
        // move projectile in direction
        gameObject.transform.position += speed * direction * Time.deltaTime;
    }

    // TODO: add collision logic
}