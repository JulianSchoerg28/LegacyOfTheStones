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

    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ResetAllQuestProgress();
            UpdateQuestUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ResetAllQuestProgress()
    {
        foreach (Quest quest in mainQuests)
        {
            quest.currentProgress = 0;
        }

        foreach (Quest quest in sideQuests)
        {
            quest.currentProgress = 0;
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
            currentSideQuestID = questID;
            activeSideQuest = true;
            UpdateQuestUI();

            Quest quest = sideQuests[currentSideQuestID];
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
    

    private void UpdateQuestUI()
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

        if (currentMainQuest != null)
        {
            questTitleText.text = currentMainQuest.questName;
            questDescriptionText.text = currentMainQuest.description;
            questProgressText.text = $"{currentMainQuest.currentProgress}/{currentMainQuest.targetAmount}";
        }
        else
        {
            questTitleText.text = "Keine aktive Quest";
            questDescriptionText.text = "";
            questProgressText.text = "";
        }

        if (currentSideQuest != null)
        {
            SidequestPanel.SetActive(true);
            sidequestTitleText.text = currentSideQuest.questName;
            sidequestDescriptionText.text = currentSideQuest.description;
            sidequestProgressText.text = $"{currentSideQuest.currentProgress}/{currentSideQuest.targetAmount}";
        }
        else
        {
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

    public void finishedQuest(int questID, bool isMainQuest)
    {
        //TODO: add reward
        
       
        
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
