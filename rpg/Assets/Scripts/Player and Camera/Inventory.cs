using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //UI
    public GameObject slotPrefab; 
    public Transform inventoryPanel; 
    public GameObject inventoryContainer; 
     
    private Image[] slotImages;
    
    //UI item description
    public GameObject descriptionPanel; 
    public Text descriptionText; 


    
    //logic
    private static int inventorySize = 8;
    private static int maxStackSize = 6;
    private Item[] inventory = new Item[inventorySize];
    private int[] itemAmount = new int[inventorySize];
    
    public static Inventory Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
    }
    

    void Start()
    {
        InitializeUI();
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        


    }
    
    private void ToggleInventory()
    {
        inventoryContainer.SetActive(!inventoryContainer.activeSelf);

        if (inventoryContainer.activeSelf)
        {
            UpdateUI();
        }
    }

    public void InitializeUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        slotImages = new Image[inventorySize];

        Debug.Log($"Vor Slot-Erstellung: inventoryPanel hat {inventoryPanel.childCount} Kinder.");
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);
            
            Debug.Log($"Slot {i} erstellt.");
            
            InventorySlot slotScript = newSlot.AddComponent<InventorySlot>();
            slotScript.slotIndex = i; 
            slotScript.descriptionPanel = descriptionPanel;
            slotScript.descriptionText = descriptionText;
            slotImages[i] = newSlot.transform.Find("ItemImage").GetComponent<Image>();
        }

        Debug.Log($"Nach Slot-Erstellung: inventoryPanel hat {inventoryPanel.childCount} Kinder.");
        
    }

    
    void UpdateUI()
    {   
        
        for (int i = 0; i < inventorySize; i++)
        {
            // if (i >= inventoryPanel.childCount)
            // {
            //     Debug.LogError($"Slot {i} existiert nicht im inventoryPanel.");
            //     return;
            // }
            
            Transform currentSlot = inventoryPanel.GetChild(i);
            Image img = currentSlot.Find("ItemImage")?.GetComponent<Image>();
            Text stackText = currentSlot.Find("StackAmount")?.GetComponent<Text>();

            if (inventory[i] != null)
            {
                if (img != null)
                {
                    img.sprite = inventory[i].ItemSprite;
                    img.enabled = true; 
                }

                if (stackText != null)
                {
                    stackText.text = itemAmount[i].ToString();
                    stackText.gameObject.SetActive(true);
                }
            }
            else
            {
                if (img != null)
                {
                    img.sprite = null;
                    img.enabled = false; 
                }

                if (stackText != null)
                {
                    stackText.text = "";
                    stackText.gameObject.SetActive(false);
                }
            }
        }
        Debug.Log($"inventoryPanel hat {inventoryPanel.childCount} Kinder.");

    }

    
    
    //logic
    public Item GetItemInSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < inventory.Length)
        {
            return inventory[slotIndex];
        }
        Debug.LogError($"Invalid inventory slot index: {slotIndex}");
        return null;
    }
    
    public int GetItemAmount(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < itemAmount.Length)
        {
            return itemAmount[slotIndex];
        }
        return 0;
    }


    public void AddToInventory(Item item)
    { 
        for (int i = 0; i < inventory.Length; i++)
        {
                if (inventory[i] != null && item.IsStackable && inventory[i].IsSameItem(item) && itemAmount[i] < maxStackSize)
                {
                    Debug.Log($"Added {item.ItemName} to inventory at slot {i}.");
                    itemAmount[i] += 1;
                    item.InvSlot = i;
                    UpdateUI();
                    return;
                } 
        }
        
        //add item in a new slot
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null) 
            {
                inventory[i] = item; 
                itemAmount[i] += 1;
                item.InvSlot = i;
                UpdateUI();
                Debug.Log($"Added {item.ItemName} to inventory at slot {i}.");
                return; 
            }
        }
        
        Debug.Log("Inventory is full");
        //TODO: add logic to drop items? 
        
    }

    
    //only needed for debuging
    public void ShowInventoryInConsole()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                Debug.Log("Item " + i + ": " + inventory[i].ItemName + ", Amount: " + itemAmount[i]);
            }
            else
            {
                Debug.Log("Item " + i + ": null");
            }
        }
    }

    public void RemoveFromInventory(Item item)
    {
        if (item.InvSlot >= 0 && item.InvSlot < inventory.Length)
        {
            int slotIndex = item.InvSlot;

            if (inventory[slotIndex] != null && inventory[slotIndex].IsSameItem(item))
            {
                if (itemAmount[slotIndex] == 1)
                {
                    inventory[slotIndex] = null; 
                }

                itemAmount[slotIndex] -= 1;

                UpdateUI();

                Debug.Log($"Removed from Inventory: {item.ItemName}. New Item Amount: {itemAmount[slotIndex]}");
                
            }
            else
            {
                Debug.LogError($"Slot {slotIndex} does not contain the specified item: {item.ItemName}");
            }
        }
        else
        {
            Debug.LogError($"Item {item.ItemName} has an invalid slot index: {item.InvSlot}");
        }
    }

    public int HowManyItemsOfThisTypeTF(int itemID)
    {
        int totalAmount = 0;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i].ItemID == itemID)
            {
                totalAmount += itemAmount[i];

            }
        }
        return totalAmount;
    }
    
    public void DeleteCoinsIDK(int itemID, int amount)
    {
        int totalAmount = 0;
        List<Item> items = new List<Item>();
        List<int> itemAmounts = new List<int>();
        
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i].ItemID == itemID)
            {
                items.Add(inventory[i]);
                itemAmounts.Add(itemAmount[i]);
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            for (int j = 0; j < itemAmounts[i]; j++)
            {
                if (amount == 0)
                {
                    return;
                }
                RemoveFromInventory(items[i]);
                amount--;
            }
        }
    }
    
    public bool HasAllRequiredStones(int fireID, int earthID, int waterID, int airID)
    {
        return HowManyItemsOfThisTypeTF(fireID) > 0 &&
               HowManyItemsOfThisTypeTF(earthID) > 0 &&
               HowManyItemsOfThisTypeTF(waterID) > 0 &&
               HowManyItemsOfThisTypeTF(airID) > 0;
    }
    
    
}
