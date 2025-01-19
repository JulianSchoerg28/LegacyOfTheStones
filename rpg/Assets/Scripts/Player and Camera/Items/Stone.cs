using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Item
{
    public string StoneName;
    public Stone(string StoneName)
        : base("Stone", "", 7, null, false, false)
    {
        this.StoneName = StoneName;
        Description = "Stone: " + StoneName;

        Sprite[] sprites = Resources.LoadAll<Sprite>("ItemsPictures/elementstones"); // "elementstones" ist der Name des Sprite-Atlas

        ItemSprite = Array.Find(sprites, sprite => sprite.name == StoneName);

        if (ItemSprite == null)
        {
            Debug.LogError($"Sprite with name '{StoneName}' not found in sprite atlas!");
        }
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
