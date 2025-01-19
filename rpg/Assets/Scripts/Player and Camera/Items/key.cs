using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Key : Item
{
    public string DoorToUnlock { get; private set; }
    public float UnlockRange = 2f; 

    public Key(string doorToUnlock)
        : base("Key", "A key to unlock a door.", 3 , Resources.Load<Sprite>("ItemsPictures/Key") , false, false)
    {
        DoorToUnlock = doorToUnlock;
    }

    public override void Use()
    {
        Debug.Log($"Trying to unlock: {DoorToUnlock}");
        Unlock(DoorToUnlock); 
    }

    private void Unlock(string doorToUnlock)
    {
        GameObject door = GameObject.Find(doorToUnlock);
        if (door != null)
        {
            Door doorScript = door.GetComponent<Door>();
            if (doorScript != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    float distance = Vector3.Distance(player.transform.position, door.transform.position);
                    if (distance <= UnlockRange)
                    {
                        if (doorScript.Unlock())
                        {
                            doorScript.Unlock();
                            
                            DestroyAfterUse = true;
                        }
                        
                        Debug.Log($"Unlocked door: {doorToUnlock}");
                    }
                    else
                    {
                        Debug.Log("You are too far from the door to unlock it.");
                    }
                }
                else
                {
                    Debug.LogError("Player not found!");
                }
            }
            else
            {
                Debug.LogWarning($"No Door script found on {doorToUnlock}");
            }
        }
        else
        {
            Debug.LogWarning($"No door found with name: {doorToUnlock}");
        }
    }
}


