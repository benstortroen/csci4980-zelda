using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public Inventory inventory;
    public HealthComponent health;

    public ItemManager itemManager;
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
            temp_text += "     Health: ";
            temp_text += health.GetHealth() + "/" + health.max_hp;
            temp_text += "\nMain Item: " + itemManager.getMainItemName();
            temp_text += "     Alt Item: " + itemManager.getAltItemName();

        }

        text_component.text = temp_text;
    }
}
