using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Inventory inventory;   
    public int slotIndex; 
    public GameObject descriptionPanel; 
    public Text descriptionText; 

    private void Start()
    {
        inventory = Inventory.Instance; 
        descriptionPanel.GetComponent<Image>().raycastTarget = false;
        descriptionText.raycastTarget = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UseItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem();
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item = inventory.GetItemInSlot(slotIndex); 
        if (item != null)
        {
            Vector3 slotPosition = transform.position;
            Vector3 offset = new Vector3(0, -100, 0); 
            descriptionPanel.transform.position = slotPosition + offset;
            
            descriptionText.text = item.Description; 
            descriptionPanel.SetActive(true); 
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false); 
    }

    private void UseItem()
    {
        Item item = inventory.GetItemInSlot(slotIndex);
        int stackAmount = inventory.GetItemAmount(slotIndex);

        if (item != null)
        {
            int remaining = QuickSlotsManager.Instance.AddItemToQuickSlot(item, stackAmount);

            int itemsToRemove = stackAmount - remaining;
            for (int i = 0; i < itemsToRemove; i++)
            {
                inventory.RemoveFromInventory(item);
            }

            Debug.Log($"{itemsToRemove} {item.ItemName} zum Quickslot hinzugefügt.");
        }
        descriptionPanel.SetActive(false); 

        
    }

    private void DropItem()
    {
        Item item = inventory.GetItemInSlot(slotIndex);
        if (item != null)
        {
            inventory.RemoveFromInventory(item);
            Debug.Log($"Dropped item: {item.ItemName}");
        }
        else
        {
            Debug.Log("No item in this slot to drop");
        }
        descriptionPanel.SetActive(false); 

    }
}