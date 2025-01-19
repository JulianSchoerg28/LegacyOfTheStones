using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : Item
{
    //if healing amount is set to 0 you´ll get a random standard heal (see possibleHealingamounts)
    private int HealingAmount { get; set; }
    
    
    public HealingPotion(int healingAmount)
        : base("Healing Potion", "", 1,
            Resources.Load<Sprite>("ItemsPictures/HealingPotion"), true, true)
    {
        int[] possibleHealingAmounts = { 50, 100 }; 
        HealingAmount = healingAmount <= 0 
            ? possibleHealingAmounts[Random.Range(0, possibleHealingAmounts.Length)] 
            : healingAmount;
        if (HealingAmount < 0 || HealingAmount > 100)
        {
            Debug.LogError("Healing Amount must be between 0 and 100. Defaulting to 50.");
            HealingAmount = 50;
        }
        
        Description = "This is a healing potion. Gives the Player " +HealingAmount + " HP";
    }
    
    public override bool IsSameItem(Item other)
    {
        return base.IsSameItem(other) && other is HealingPotion potion && potion.HealingAmount == HealingAmount;
    }
    
    public override void Use()
    {
        GameManager.Instance.takeHeal(HealingAmount);
    }
    
}

