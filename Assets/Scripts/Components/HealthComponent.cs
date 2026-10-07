using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public float max_hp = 5;
    protected float current_hp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_hp = this.max_hp;
    }

    public void RestoreHealth(float h)
    {
        current_hp += h;
        current_hp = current_hp > max_hp ? max_hp : current_hp;
    }

    public virtual void DealDamage(float damage)
    {
        current_hp -= damage;
        if (current_hp <= 0)
        {
            OnDeath();
        }
    }

    public virtual float GetHP()
    {
        return current_hp;
    }

    public virtual void OnDeath()
    {
        Debug.Log("gameObject died");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public float GetHealth()
    {
        return current_hp;
    }
}
