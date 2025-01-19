using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "QuestSystem/Quest")]
public class Quest : ScriptableObject
{
    public int questID;
    public string questName;
    public string description;
    
    public int targetAmount;
    public int currentProgress;
    public bool isCompleted;
    public bool isMainQuest; 
    
    public void UpdateProgress(int value)
    {
        currentProgress += value; 
        if (currentProgress >= targetAmount)
        {
            Complete(); 
        }
    }

    public void Complete()
    {
        isCompleted = true;
        QuestManager.Instance.finishedQuest(questID, isMainQuest);
        Debug.Log($"{questName} is completed!");
    }
}