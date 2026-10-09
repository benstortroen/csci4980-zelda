using System.Collections;
using System.Data;
using UnityEditor.Toolbars;
using UnityEngine;

public class BombProjectile : MonoBehaviour, IItem
{
    public int Damage { get; set; } = 4;
    public string ItemName { get; set; } = "Bomb";
    public void UseItem(Vector3 position, Vector2 direction){}

    private float bomb_timer = 2.5f;
    private float explode_duration = 1.0f;

    // components
    private BoxCollider2D col;
    private Animator animator;


    // player instantiates a bomb in the dungeon
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        col.enabled = false;
        
        StartCoroutine(Explode());
    }

    // explode after 2.5 seconds
    // enable damage trigger 
    // play explode animation 
        // disable bomb sprite
        // flash puffs of smoke
    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(bomb_timer);

        animator.Play("bomb_explode");
        col.enabled = true;

        yield return new WaitForSeconds(explode_duration);
        Destroy(gameObject);
    }
    
}
