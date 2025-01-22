using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [SerializeField] private GameObject dialogUI;
    [SerializeField] private Text dialogText;
    [SerializeField] private Text dialogNameText;

    private Queue<string> sentences;
    private bool isDialogActive = false;
    
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
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (isDialogActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialog(Dialog dialog)
    {
        if (isDialogActive)
        {
            CloseDialog();
        }

        dialogUI.SetActive(true);
        sentences.Clear();
        isDialogActive = true;

        if (dialogNameText != null)
        {
            dialogNameText.text = dialog.npcName;
        }

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            CloseDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    public void CloseDialog()
    {
        Debug.Log("Dialog wird geschlossen.");
        dialogUI.SetActive(false);
        isDialogActive = false;
    }
}
