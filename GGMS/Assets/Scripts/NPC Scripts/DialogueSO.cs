using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DialogueSO",menuName ="Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;
}

[System.Serializable]
public class DialogueLine 
{
    public NPCso speaker;
    [TextArea(1,5)]public string text;
}

[System.Serializable]
public class DialogueOption 
{
    public string optionText;
    public DialogueSO nextDialogue;
}
