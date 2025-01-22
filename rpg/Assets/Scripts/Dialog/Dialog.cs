using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog System/Dialog")]
public class Dialog : ScriptableObject {
    public string npcName; 
    [TextArea(3, 10)]
    public string[] sentences;

    public int questId = -1;
    public bool isMainQuest = false;
}

