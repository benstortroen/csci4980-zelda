using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int max_hp = 5;
    private int current_hp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_hp = this.max_hp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DealDamage(int d)
    {
        current_hp -= d;
        if (current_hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void RestoreHealth(int h)
    {
        current_hp += h;
        current_hp = current_hp > max_hp ? max_hp : current_hp;
    }

    public int GetHealth()
    {
        return current_hp;
    }
}
