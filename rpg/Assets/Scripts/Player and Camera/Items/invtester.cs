using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invtester : MonoBehaviour
{
    private Inventory inv;

    void Start()
    {
        // inv = Inventory.Instance;
        //
        // if (inv == null)
        // {
        //     Debug.LogError("Inventar wurde nicht korrekt initialisiert!");
        //     return;
        // }
        //
        // Debug.Log("Füge Key hinzu...");
        // Key key = new Key("secretDoor1");
        // inv.AddToInventory(key);
        //
        // Debug.Log("Füge Sword hinzu...");
        // inv.AddToInventory(new Sword("stone"));
        // inv.AddToInventory(new Sword("wood"));
        // inv.AddToInventory(new Sword("iron"));
        //
        // Debug.Log("Füge HealingPotion hinzu...");
        // for (int i = 0; i < 50; i++)
        // {
        //     HealingPotion healingPotion = new HealingPotion(0);
        //     inv.AddToInventory(healingPotion);
        // }
        inv = Inventory.Instance;
        StartCoroutine(AddItemsWithDelay());
    }

    private IEnumerator AddItemsWithDelay()
    {
        yield return new WaitForSeconds(2f); // Warte 0.2 Sekunden
        // Key hinzufügen
        Key key = new Key("secretDoor1");
        inv.AddToInventory(key);
        inv.AddToInventory(new Sword("stone"));
        inv.AddToInventory(new Sword("wood"));
        inv.AddToInventory(new Sword("iron"));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            inv.AddToInventory(new HealingPotion(0));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            inv.AddToInventory(new Coin());
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            inv.AddToInventory(new Sword("stone"));
        }
    }
}
