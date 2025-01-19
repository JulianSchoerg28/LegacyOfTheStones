using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Armor: Item
{
    public float Defense { get; private set; }
    public int Durability;

    public Armor(string type)
        : base("Armor", "", 2, null, false, true)
    {
        switch (type)
        {
            case "bronze":
                ItemSprite = Resources.Load<Sprite>("ItemsPictures/BronzeArmor");
                Defense = 0.1f;
                Durability = 50;
                break;
            case "silver":
                ItemSprite = Resources.Load<Sprite>("ItemsPictures/SilverArmor");
                Defense = 0.2f;
                Durability = 75;
                break;
            case "gold": 
                ItemSprite = Resources.Load<Sprite>("ItemsPictures/GoldArmor");
                Defense = 0.3f;
                Durability = 100;
                break;
            default:
                Debug.LogError("Yeah No, wrong armor type!");
                break;
        }
        
        Description =$"This is an armor with a Duribility of {Durability}! Player get´s {Defense * 100}% less damage";
    }

    public override void Use()
    {
        GameManager.Instance.EquipArmor(this); 
    }

    public void updateDuribilityInDescription()
    {
        Description =$"This is an armor with a Duribility of {Durability}! Player get´s {Defense * 100}% less damage";
    }
    
}
