using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public List<Quest> mainQuests = new List<Quest>();
    public List<Quest> sideQuests = new List<Quest>();
    public int currentMainQuestID = -1; 
    public int currentSideQuestID = -1; 
    
    public Text questTitleText;
    public Text questDescriptionText;
    public Text questProgressText;

    public GameObject SidequestPanel;
    public Text sidequestTitleText;
    public Text sidequestDescriptionText;
    public Text sidequestProgressText;
    
    private bool activeMainQuest;
    private bool activeSideQuest;

    public bool ActiveMainQuest => activeMainQuest;

    public bool ActiveSideQuest => activeSideQuest;

    public static QuestManager Instance { get; private set; }

    public GameObject rewardPrefab;
    
    private bool initialized = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            // ResetAllQuestProgress();
            
            if (!initialized)
            {
                ResetAllQuestProgress();
                initialized = true;
            }
            UpdateQuestUI();
            Debug.Log("yeah");
        }
        else
        {
            Debug.Log("Duplicate Instance");
            Destroy(gameObject);
        }
    }

    // private void Update()
    // {
    //     UpdateQuestUI();
    // }

    public void ResetAllQuestProgress()
    {
        foreach (Quest quest in mainQuests)
        {
            quest.currentProgress = 0;
            quest.isCompleted = false;
        }

        foreach (Quest quest in sideQuests)
        {
            quest.currentProgress = 0;
            quest.isCompleted = false;
        }
    }


    public void UpdateMainQuest()
    {
        if (!activeMainQuest && currentMainQuestID + 1 < mainQuests.Count)
        {
            currentMainQuestID += 1;
            activeMainQuest = true;
            UpdateQuestUI();
            
            Quest quest = mainQuests[currentMainQuestID];
            Debug.Log($"{quest.questName} (Main Quest) is now active.");
        }
    }

    public void UpdateSideQuest(int questID)
    {
        
        if (!activeSideQuest) 
        {
            Quest quest = sideQuests[questID];
            if (quest == null || quest.isCompleted)
            {
                return;
            }
            
            
            currentSideQuestID = questID;
            activeSideQuest = true;
            UpdateQuestUI();

            Debug.Log($"{quest.questName} (Side Quest) is now active.");
        }
    }


    public Quest GetQuestByID(int questID, bool isMainQuest)
    {
        if (isMainQuest)
        {
            if (questID >= 0 && questID < mainQuests.Count)
                return mainQuests[questID];
        }
        else
        {
            if (questID >= 0 && questID < sideQuests.Count)
                return sideQuests[questID];
        }

        return null;
    }
    
    // public void FindAndAssignUIElements()
    // {
    //     // Suche die neuen UI-Elemente in der aktuellen Szene
    //     questTitleText = GameObject.Find("QuestName")?.GetComponent<Text>();
    //     questDescriptionText = GameObject.Find("QuestDescription")?.GetComponent<Text>();
    //     questProgressText = GameObject.Find("QuestProgress")?.GetComponent<Text>();
    //
    //     SidequestPanel = GameObject.Find("SideQuestBackgroundPanel");
    //     sidequestTitleText = GameObject.Find("SideQuestName")?.GetComponent<Text>();
    //     sidequestDescriptionText = GameObject.Find("SideQuestDescription")?.GetComponent<Text>();
    //     sidequestProgressText = GameObject.Find("SideQuestProgress")?.GetComponent<Text>();
    //
    //     Debug.Log($"UI-Referenzen aktualisiert. Main Quest UI: {questTitleText != null}, Side Quest UI: {sidequestTitleText != null}");
    // }

    public void FindAndAssignUIElements()
    {
        // GameObject mainCanvas = GameObject.FindWithTag("MainCanvas");
        // if (mainCanvas != null)
        // {
        //     questTitleText = mainCanvas.transform.Find("QuestTitleText")?.GetComponent<Text>();
        //     questDescriptionText = mainCanvas.transform.Find("QuestDescriptionText")?.GetComponent<Text>();
        //     questProgressText = mainCanvas.transform.Find("QuestProgressText")?.GetComponent<Text>();
        // }
    }
    


    public void UpdateQuestUI()
    {
        Quest currentMainQuest = null;
        Quest currentSideQuest = null;
        
        if (activeMainQuest)
        {
            currentMainQuest = GetQuestByID(currentMainQuestID, true);
        } 
        
        if (activeSideQuest)
        {
            currentSideQuest = GetQuestByID(currentSideQuestID, false);
        }
        
        if (currentMainQuest) 
            Debug.Log(currentMainQuest.questName);

        if (currentMainQuest != null)
        {
            Debug.Log("there is a main quest");
            Debug.Log(questTitleText.text);
        
            questTitleText.text = currentMainQuest.questName;
            questDescriptionText.text = currentMainQuest.description;
            questProgressText.text = $"{currentMainQuest.currentProgress}/{currentMainQuest.targetAmount}";
        }
        else
        {
            Debug.Log("there is no main quest");
            Debug.Log(questTitleText.text);
            questTitleText.text = "Keine aktive Quest";
            questDescriptionText.text = "";
            questProgressText.text = "";
        }
        
        if (currentSideQuest) 
            Debug.Log(currentSideQuest.questName);
        
        if (currentSideQuest != null)
        {
            Debug.Log("there is a side quest");

            SidequestPanel.SetActive(true);
            sidequestTitleText.text = currentSideQuest.questName;
            sidequestDescriptionText.text = currentSideQuest.description;
            sidequestProgressText.text = $"{currentSideQuest.currentProgress}/{currentSideQuest.targetAmount}";
        }
        else
        {
            Debug.Log("there is no side quest");

            SidequestPanel.SetActive(false);
        }
    }

    public void UpdateProgress(int questID, int value, bool isMainQuest)
    {
        Quest quest = null;

        if(isMainQuest)
        {
            if (questID >= 0 && questID < mainQuests.Count)
            {
                quest = mainQuests[questID]; 
            }
                
        }
        else 
        {
            if (questID >= 0 && questID < sideQuests.Count)
            {
                quest = sideQuests[questID]; 
            }
        }

        if (quest != null)
        {
            quest.UpdateProgress(value); 
        }
        
        UpdateQuestUI();
    }


    public int GetCurrentQuestID(bool mainQuest)
    {
        if (mainQuest && activeMainQuest)
        {
            return currentMainQuestID;
        }else if (!mainQuest && activeSideQuest)
        {
            return currentSideQuestID;
        }

        return -1;
    }
    
    public Quest GetQuestByIDFromMainQuests(int questId)
    {
        foreach (Quest quest in mainQuests)
        {
            if (quest.questID == questId)
            {
                return quest;
            }
        }
        return null;
    }

    public Quest GetQuestByIDFromSideQuests(int questId)
    {
        foreach (Quest quest in sideQuests)
        {
            if (quest.questID == questId)
            {
                return quest;
            }
        }
        return null;
    }
    
    public bool IsMainQuestActive(int questId)
    {
        foreach (Quest quest in mainQuests)
        {
            if (quest.questID == questId && !quest.isCompleted)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsSideQuestActive(int questId)
    {
        foreach (Quest quest in sideQuests)
        {
            if (quest.questID == questId && !quest.isCompleted)
            {
                return true;
            }
        }
        return false;
    }


    

    public void finishedQuest(int questID, bool isMainQuest)
    {
        //TODO: add reward
        if (isMainQuest)
        {
            
        }
        else
        {
           switch (questID)
           
           {
               case 0:
                   Vector3 spawnPos = new Vector3(0, 0, 0);
                   Vector3 offset = new Vector3(-1f, 0f, 0f); 
    
                   GameObject npc = GameObject.Find("npc_Anna");
            
                   if (npc != null)
                   {
                       spawnPos = npc.transform.position + offset;
                   }
                   
                   Debug.Log("Hier");
                   Instantiate(rewardPrefab, spawnPos, Quaternion.identity);
                   break;
           } 
        }
        
        
        if (isMainQuest)
        {
            activeMainQuest = false;
        }else if (!isMainQuest)
        {
            activeSideQuest = false;
        }
        
        UpdateQuestUI();
        
        
    }

}
