using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public int questID;
    private QuestManager questManager;
    public bool destroyOnCollect = true;
    public bool isMainQuest;

    void Start()
    {
        questManager = QuestManager.Instance;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("MQ: " + questManager.currentMainQuestID + " SQ: " + questManager.currentSideQuestID +" Quest ID: " + questID);
            
            if ((questManager.currentMainQuestID == questID && isMainQuest)|| (questManager.currentSideQuestID == questID && !isMainQuest))
            {
                questManager.UpdateProgress(questID, 1, isMainQuest);

                if (destroyOnCollect)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Item collected but not destroyed.");
                }
            }
            else
            {
                Debug.Log($"This item is not active yet as the corresponding quest (ID: {questID}) has not started.");
            }
        }
    }
}