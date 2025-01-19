using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public Coin()
        : base("Coin", "A Coin to buy things :D", 8, Resources.Load<Sprite>("ItemsPictures/Coin"), true, false)
    {
        
    }

    public override void Use()
    {
        
    }
}
