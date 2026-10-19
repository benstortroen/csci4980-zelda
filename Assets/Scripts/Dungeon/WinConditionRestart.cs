using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConditionRestart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("Dungeon");
        }
    }
}
