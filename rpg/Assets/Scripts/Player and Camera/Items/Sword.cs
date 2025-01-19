using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sword : Item
{
    public int damage;
    public int Durability;

    public Sword(string type)
        : base("Sword", "", 4, null, false, false)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("ItemsPictures/SwordSprites");
        switch (type)
        {
            case "wood":
                ItemSprite = System.Array.Find(sprites, sprite => sprite.name == "Sword1");
                damage = 10;
                Durability = 100;
                break;
            case "stone":
                ItemSprite = System.Array.Find(sprites, sprite => sprite.name == "Sword2");
                damage = 25;
                Durability = 200;
                break;
            case "iron":
                ItemSprite = System.Array.Find(sprites, sprite => sprite.name == "Sword6");
                damage = 50;
                Durability = 500;
                break;
            default:
                Debug.LogError("Yeah No, wrong armor type!");
                break;
        }
        
        Description =$"This is a sword with {damage} Attack Damage and a Duribility of {Durability}!";

    }
    
    public override void Use()
    {
        Durability--;
        updateDuribilityInDescription();
        Debug.Log("used sword");
        //TODO: attack 
    }
    
    public void updateDuribilityInDescription()
    {
        Description =$"This is a sword with {damage} Attack Damage and a Duribility of {Durability}!";
    }
}
