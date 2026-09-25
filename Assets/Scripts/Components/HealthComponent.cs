using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public int max_hp = 5;
    private int current_hp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_hp = this.max_hp;
    }

    public void RestoreHealth(int h)
    {
        current_hp += h;
        current_hp = current_hp > max_hp ? max_hp : current_hp;
    }

    public void DealDamage(int damage)
    {
        current_hp -= damage;
        if (current_hp <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        Debug.Log("gameObject died");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetHealth()
    {
        return current_hp;
    }
}
