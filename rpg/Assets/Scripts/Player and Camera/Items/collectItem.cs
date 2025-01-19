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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
                    if (additionalInt == 0) additionalInt = 0;
                    
                    item = new HealingPotion(additionalInt);
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
            
            Debug.Log("Item collected");
            Inventory.Instance.AddToInventory(item); 
            Destroy(gameObject); 
            
        }
    }
}
