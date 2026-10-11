using System.Collections.Generic;
using UnityEngine;

public class RoomClearBehavior : MonoBehaviour
{

    private List<GameObject> enemies;
    [SerializeField] private GameObject loot;
    [SerializeField] private Transform lootSpawnPoint;

    private bool cleared = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (loot != null)
        {
            enemies = new List<GameObject>();
            foreach (Transform child in transform)
            {
                if (child.CompareTag("enemy"))
                {
                    cleared = false;
                    enemies.Add(child.gameObject);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!cleared)
        {

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].GetComponent<EnemyHealthComponent>().IsDead())
                {

                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                }
            }
            if (enemies.Count <= 0)
            {
                cleared = true;
                AudioManager.Instance.PlayLevelClear(transform.position);
                Instantiate(loot, lootSpawnPoint.position, Quaternion.identity);
            }
        }
    }
}
