using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotsManager : MonoBehaviour
{
    public static QuickSlotsManager Instance { get; private set; }

    public Transform quickslotsParent;
    private QuickSlot[] quickSlots;
    
    private int maxStackSize = 2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        quickSlots = quickslotsParent.GetComponentsInChildren<QuickSlot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseQuickSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseQuickSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UseQuickSlot(2);
    }

    private void UseQuickSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < quickSlots.Length)
        {
            quickSlots[slotIndex].UseItem();
        }
    }
    
    public int AddItemToQuickSlot(Item item, int amount)
    {
        int remainingAmount = amount;
        
        foreach (QuickSlot slot in quickSlots)
        {
            if (slot.IsSameItem(item) && slot.CanAddToStack(maxStackSize) && item.IsStackable)
            {
                int added = slot.AddItem(item, remainingAmount, maxStackSize);
                remainingAmount -= added;

                if (remainingAmount <= 0)
                    return 0; 
            }
        }

        foreach (QuickSlot slot in quickSlots)
        {
            if (slot.IsEmpty())
            {
                int added = slot.AddItem(item, remainingAmount, maxStackSize);
                remainingAmount -= added;

                if (remainingAmount <= 0)
                    return 0; 
            }
        }

        return remainingAmount;
    }
    
    
}
