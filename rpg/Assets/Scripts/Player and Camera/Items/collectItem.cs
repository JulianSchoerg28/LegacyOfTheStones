using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectItem : MonoBehaviour
{
    public int itemID;
    public int additionalInt;
    public string additionalString;
    public int questID = -1;
    public bool isMainQuest = false;

    public int xpReward = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Quest-Logik
            if (questID != -1)
            {
                int currentQuestID = QuestManager.Instance.GetCurrentQuestID(isMainQuest);

                if (currentQuestID == -1)
                {
                    return;
                }

                if (questID != currentQuestID)
                {
                    return;
                }
            }

            Item item = null;

            switch (itemID)
            {
                case 1:
                    Debug.Log("Heal");

                    item = new HealingPotion(additionalInt);
                    break;
                case 2:
                    item = new Armor(additionalString);
                    break;
                case 3:
                    item = new Key(additionalString);
                    break;
                case 4:
                    item = new Sword(additionalString);
                    break;
                case 5: 
                    Debug.Log("Boost");
                    item = new BoostPotion();
                    break;
                case 7:
                    item = new Stone(additionalString);
                    break;
                case 8:
                    item = new Coin();
                    break;
            }

            if (item == null)
            {
                Debug.Log("Item not found");
                return;
            }

            Debug.Log("Item " + item.ItemName + " collected");
            Inventory.Instance.AddToInventory(item);
            
            GiveXP();
            
            Destroy(gameObject); 
        }
    }


    private void GiveXP()
    {
        if (XPManager.instance != null)
        {
            XPManager.instance.AddXP(xpReward);
            Debug.Log("Spieler hat " + xpReward + " XP f�r das Sammeln eines Items erhalten!");
        }
        else
        {
            Debug.LogError("XPManager nicht gefunden! Stelle sicher, dass ein XPManager in der Szene vorhanden ist.");
        }
    }
}

