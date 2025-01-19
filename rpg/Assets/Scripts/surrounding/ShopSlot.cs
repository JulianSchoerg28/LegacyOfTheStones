using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
    public int price;
    public int itemID;

    private Item coin;
    // Start is called before the first frame update
    void Start()
    {
        coin = new Coin();
    }

   
    public void OnPointerClick(PointerEventData eventData)
    {
        Item item;
        
        switch (itemID)
        {
            case 2:
                item = new Armor("bronze");
                break;
            case 1:
                item = new HealingPotion(0);
                break;
            default:
                return;
        }
        Debug.Log("Hier");
        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(Inventory.Instance.HowManyItemsOfThisTypeTF(8));
            if (Inventory.Instance.HowManyItemsOfThisTypeTF(8) >= price)
            {
                Debug.Log("Heyy");
                Inventory.Instance.DeleteCoinsIDK(8, price);
                Inventory.Instance.AddToInventory(item);
            }
        }

    }
}
