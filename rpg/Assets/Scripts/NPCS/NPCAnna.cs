using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCAnna : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
             QuestManager.Instance.UpdateSideQuest(0);
             // questManager.UpdateMainQuest();
        }
      
    }

 
}


