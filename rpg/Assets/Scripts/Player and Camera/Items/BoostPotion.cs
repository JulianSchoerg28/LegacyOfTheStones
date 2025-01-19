using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPotion : Item
{
    private int boostDuration = 30;
    private float boostMultiplier = 1.5f; 
    
    public BoostPotion()
        : base("Healing Potion", "This is a BoostPotion which gives the player a boost in walking Speed and Damage by 20% for 30 Seconds.",
            1, null, true, true)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("ItemsPictures/smallPotions");
        ItemSprite = System.Array.Find(sprites, sprite => sprite.name == "small Potions_16");
    }
    
    public override void Use()
    {
        GameManager.Instance.StartSpeedBoost(boostMultiplier, boostDuration);       
    }
    
}
