using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public float max_hp = 5;

    public float flashRate = 0.2f;

    private SpriteRenderer spriteRenderer;
    protected float current_hp;

    public float iWindow = 0.4f;
    private float iWindowStart = 0.0f;

    protected bool iWindowActive = false;
    private float flashStartTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            spriteRenderer.color = new Color(255 / 255f, 124 / 255f, 62 / 255f);
        }

    }

    protected virtual void Update()
    {
        if (iWindowActive)
        {
            float dt = Time.time - flashStartTime;
            if (dt < flashRate)
            {
                spriteRenderer.color = new Color(255 / 255f, 0 / 255f, 0 / 255f);
            }
            else
            {
                spriteRenderer.color = new Color(255 / 255f, 181 / 255f, 146 / 255f);
                if (dt > 2 * flashRate)
                {
                    flashStartTime = Time.time;
                }

            }

            if (Time.time - iWindowStart > iWindow)
            {
                iWindowActive = false;
                spriteRenderer.color = new Color(1, 1, 1);
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
