using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public float max_hp = 5;

    [SerializeField] private Sprite iWindowSprite;
    protected float current_hp;

    public float iWindow = 0.75f;
    private float iWindowStart = 0.0f;

    protected bool iWindowActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
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
        if (!iWindowActive)
        {
            current_hp -= damage;
            if (current_hp <= 0)
            {
                OnDeath();
            }
            iWindowStart = Time.time;
            iWindowActive = true;
        }

    }

    protected virtual void Update()
    {
        if (iWindowActive)
        {
            if (Time.time - iWindowStart > iWindow)
            {
                iWindowActive = false;
            }
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

    public bool IsFull()
    {
        return current_hp == max_hp;
    }

    public bool IsDead()
    {
        return current_hp <= 0;
    }

    public bool IsInvulnerable()
    {
        return iWindowActive;
    }
}
