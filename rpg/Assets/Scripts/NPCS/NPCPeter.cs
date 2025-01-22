using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPeter : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            QuestManager.Instance.UpdateMainQuest();
        }
    }
    
}


