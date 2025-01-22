using UnityEngine;

public class NPCBase : MonoBehaviour
{
    public Dialog[] Dialogs;
    private DialogManager dialogManager;
    private QuestManager questManager;

    void Start()
    {
        dialogManager = DialogManager.Instance;
        questManager = QuestManager.Instance;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Dialog selectedDialog = null;

            foreach (Dialog dialog in Dialogs)
            {
                if (dialog.questId == -1)
                {
                    selectedDialog = dialog;
                }
                else if (dialog.isMainQuest && questManager.IsMainQuestActive(dialog.questId))
                {
                    selectedDialog = dialog;
                    break;
                }
                else if (!dialog.isMainQuest && questManager.IsSideQuestActive(dialog.questId))
                {
                    selectedDialog = dialog;
                    break;
                }
            }

            if (selectedDialog != null)
            {
                dialogManager.StartDialog(selectedDialog);
            }
            else
            {
                Debug.LogWarning("Kein passender Dialog gefunden!");
            }
        }
    }
}