 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Item 
{
    private string itemName;
    private string description;
    private int itemID; 
    private Sprite itemSprite;
    private bool isStackable;
    private int invSlot;
    private bool destroyAfterUse;



    protected Item(string itemName, string description, int itemID, Sprite itemSprite, bool isStackable, bool destroyAfterUse)
    {
        this.itemName = itemName;
        this.description = description;
        this.itemID = itemID;
        this.itemSprite = itemSprite;
        this.isStackable = isStackable;
        invSlot = -1;
        this.destroyAfterUse = destroyAfterUse;
    }

    public string Description
    {
        get => description;
        set => description = value;
    }

    public int InvSlot
    {
        get => invSlot;
        set => invSlot = value;
    }

    // public string Description => description;
    public int ItemID => itemID;  
    public Sprite ItemSprite
    {
        get => itemSprite;
        set => itemSprite = value;
    }

    public string ItemName => itemName;
    public bool IsStackable => isStackable;
    public bool DestroyAfterUse
    {
        get => destroyAfterUse;
        set => destroyAfterUse = value;
    }

    public abstract void Use();
    
    public virtual bool IsSameItem(Item other)
    {
        return other != null && ItemID == other.ItemID;
    }


    
}   
