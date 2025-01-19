using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCAnna : MonoBehaviour
{
    private DialogManger dialogManger;
    private QuestManager questManager;

    void Start()
    {
        dialogManger = DialogManger.Instance;
        questManager = QuestManager.Instance;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
             // if (questManager.GetCurrentQuestID() < 0) {
             //     Dialog testdialog = ScriptableObject.CreateInstance<Dialog>();
             //     testdialog.sentences = new string[2]; 
             //     testdialog.npcName = "Anna";
             //     
             //     dialogManger.StartDialog(testdialog);
             // } 
             
             questManager.UpdateSideQuest(0);
        }
      
    }

 
}


