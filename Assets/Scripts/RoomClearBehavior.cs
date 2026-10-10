using System.Collections.Generic;
using UnityEngine;

public class RoomClearBehavior : MonoBehaviour
{

    private List<GameObject> enemies;
    [SerializeField] private GameObject loot;
    [SerializeField] private Transform lootSpawnPoint;

    private bool cleared = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("enemy"))
            {
                enemies.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!cleared)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<EnemyHealthComponent>().IsDead())
                {
                    enemies.Remove(enemy);
                    Destroy(enemy);
                }
            }
            if (enemies.Count == 0)
            {
                cleared = true;
                Instantiate(loot, lootSpawnPoint.position, Quaternion.identity);
            }
        }
    }
}
