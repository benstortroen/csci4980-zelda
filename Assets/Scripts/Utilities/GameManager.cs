using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject player;
    void Awake()
    {
        // Implement Resolution and Control Standards
        Screen.SetResolution(1024, 960, false);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
