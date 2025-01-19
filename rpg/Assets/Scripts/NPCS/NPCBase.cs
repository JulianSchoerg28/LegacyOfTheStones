using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour
{
    public Dialog[] Dialogs;
    private DialogManger dialogManager;
    private QuestManager questManager;
    

    void Start()
    {
        dialogManager = DialogManger.Instance;
        questManager = QuestManager.Instance;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            // if (questManager.GetCurrentQuestID() >= 0)
            // {
            //     dialogManager.StartDialog(Dialogs[questManager.GetCurrentQuestID()]);
            // }
        }
    }
    
}


