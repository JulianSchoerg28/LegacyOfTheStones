using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//File just for testing, will get delete afterwards :D

public class invtester : MonoBehaviour
{

    void Start()
    {
        Inventory inv = Inventory.Instance;
        
        // Armor gold = new Armor("gold");
        // Armor silver = new Armor("silver");
        //
        // inv.AddToInventory(gold);
        // inv.AddToInventory(silver);
        //
        Key key = new Key("secretDoor1");
        inv.AddToInventory(key);
        //
        // Sword sword1 = new Sword("wood");
        // Sword sword2 = new Sword("stone");
        // Sword sword3 = new Sword("iron");
        // inv.AddToInventory(sword1);
        // inv.AddToInventory(sword2);
        // inv.AddToInventory(sword3);
        //
        // BoostPotion boostPotion = new BoostPotion();
        // inv.AddToInventory(boostPotion);
        inv.AddToInventory(new Coin());
        inv.AddToInventory(new Coin());
        inv.AddToInventory(new Coin());
        inv.AddToInventory(new Coin());
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameManager.Instance.TakeDamage(20);
        }
    }
}
