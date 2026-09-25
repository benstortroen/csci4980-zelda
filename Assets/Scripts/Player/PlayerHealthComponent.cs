using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : HealthComponent
{
    public override void OnDeath()
    {
        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
