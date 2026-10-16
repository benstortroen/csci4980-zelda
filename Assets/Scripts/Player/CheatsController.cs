using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CheatsController : MonoBehaviour
{
    public static bool godMode = false;
    private HealthComponent healthComponent;

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            toggleGodMode();
        }
        else if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            if (SceneManager.GetActiveScene().name == "SecretScene")
            {
                SceneManager.LoadScene("Dungeon");
            }
            else
            {
                SceneManager.LoadScene("SecretScene");
            }
        }
    }

    public static void toggleGodMode()
    {
        godMode = !godMode;
        Debug.Log("God mode toggled: " + godMode);
    }
}
