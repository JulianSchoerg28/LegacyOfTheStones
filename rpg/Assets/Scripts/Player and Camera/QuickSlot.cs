using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour, IPointerClickHandler{
    public Image itemImage;
    public Text stackAmountText;

    private Item currentItem;
    private int currentStack;
    
    public bool IsEmpty() => currentItem == null;

    public bool IsSameItem(Item item) => currentItem != null && currentItem.IsSameItem(item);

    public bool CanAddToStack(int maxStackSize) => currentStack < maxStackSize;

    private void Start()
    {
        itemImage.enabled = false;
        stackAmountText.enabled = false;
    }
    
    public int AddItem(Item item, int amount, int maxStackSize)
    {
        if (IsEmpty())
        {
            currentItem = item;
            currentStack = Mathf.Min(amount, maxStackSize);
            UpdateUI();
            return currentStack;
        }

        if (IsSameItem(item))
        {
            int spaceLeft = maxStackSize - currentStack;
            int addedAmount = Mathf.Min(amount, spaceLeft);

            currentStack += addedAmount;
            UpdateUI();
            return addedAmount;
        }

        return 0;
    }
    
    private void ReturnItemToInventory()
    {
        if (currentItem != null)
        {
            for (int i = 0; i < currentStack; i++)
            {
                Inventory.Instance.AddToInventory(currentItem);

            }

            currentItem = null;
            currentStack = 0;
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (currentItem != null)
        {
            itemImage.sprite = currentItem.ItemSprite;
            itemImage.enabled = true;

            stackAmountText.text = currentStack > 1 ? currentStack.ToString() : "";
            stackAmountText.enabled = currentStack > 1;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
            stackAmountText.text = "";
            stackAmountText.enabled = false;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UseItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            ReturnItemToInventory();
        }
    }
    
    public void UseItem()
    {
        if (currentItem != null)
        {
         
            currentItem.Use();

            if (currentItem.DestroyAfterUse)
            {
                currentStack--;

                if (currentStack <= 0)
                {
                    currentItem = null; 
                    currentStack = 0; 
                }
            }
            
            UpdateUI();
        }
    }

    
    
}
