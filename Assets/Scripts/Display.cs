using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public Inventory inventory;
    public Health health;
    TextMeshProUGUI text_component;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text_component = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        string temp_text = "";
        if (inventory != null && text_component != null)
        {
            temp_text += "Rupees: ";
            temp_text += inventory.GetRupees().ToString();
            temp_text += "\n Health: ";
            temp_text += health.GetHealth() + "/" + health.max_hp;
        }

        text_component.text = temp_text;
    }
}
