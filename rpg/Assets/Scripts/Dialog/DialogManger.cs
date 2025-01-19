using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogManger : MonoBehaviour
{
    public Text dialogText; 
    public Text npcNameText; 
    private int currentSentenceIndex;
    private Dialog currentDialog;
    public GameObject bubble;
    private QuestManager questManager;
    public static DialogManger Instance { get; private set; }
    
    private Transform playerTransform; 
    public float maxDistance = 5f;   
    private Transform npcTransform;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        questManager = QuestManager.Instance;
        
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No object with tag 'Player' found!");
        }
        
    }
    
    /*public void StartDialog(Dialog dialog) 
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        npcTransform = null;
        
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(dialog.npcName))
            {
                npcTransform = obj.transform;
                break;
            }
        }
        
        if (npcTransform == null)
        {
            Debug.LogError($"NPC with name containing '{dialog.npcName}' not found.");
            return;
        }

        bubble.SetActive(true);
        currentDialog = dialog;
        npcNameText.text = dialog.npcName;
        currentSentenceIndex = 0;
        
        if (questManager.IsCurrentQuestCompleted())
        {
            dialogText.text = "";
        } 
        else
        {
            ShowNextSentence();
        }
    }

    public void ShowNextSentence() {
        if (currentSentenceIndex < currentDialog.sentences.Length) {
            dialogText.text = currentDialog.sentences[currentSentenceIndex];
            currentSentenceIndex++;
        } else {
            EndDialog();
        }
    }

    void EndDialog() {
        dialogText.text = "";
        npcNameText.text = "";
        bubble.SetActive(false);
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogText.IsActive()) 
        {
            if (questManager.IsCurrentQuestCompleted())
            {
                EndDialog();
            } 
            else
            {
                ShowNextSentence();
            }
        }
    
        if (bubble.activeSelf && Vector3.Distance(playerTransform.position, npcTransform.position) > maxDistance)
        {
            EndDialog();
        }
    }*/

}
