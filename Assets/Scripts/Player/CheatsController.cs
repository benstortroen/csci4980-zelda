using UnityEngine;
using UnityEngine.InputSystem;
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
    }

    public static void toggleGodMode()
    {
        godMode = !godMode;
    }
}
