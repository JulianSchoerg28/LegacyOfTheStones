using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPeter : MonoBehaviour
{

    private QuestManager questManager;
    private DialogManger dialogManger;
    
    void Start()
    {
        questManager = QuestManager.Instance;
        dialogManger = DialogManger.Instance;
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     { 
    //         if (questManager.currentQuestID < 0) {
    //             questManager.SetCurrentQuest(0);
    //         }else if(questManager.IsQuestCompleted(0)) {
    //             questManager.SetCurrentQuest(1);
    //         }
    //     }
    // }    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            questManager.UpdateMainQuest();
        }
    }
    
}


