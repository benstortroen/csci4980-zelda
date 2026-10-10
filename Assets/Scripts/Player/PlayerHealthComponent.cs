using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : HealthComponent
{

    StateParameters stateParameters;
    ArrowKeyMovement movement;

    protected override void Start()
    {
        base.Start();
        stateParameters = GetComponent<StateParameters>();
        movement = GetComponent<ArrowKeyMovement>();
    }

    public override void OnDeath()
    {
        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public override void DealDamage(float d)
    {
        if (!CheatsController.godMode && !stateParameters.GetKnockbackMode())
        {
            base.DealDamage(d);
        }
    }
}
