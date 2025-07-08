using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DialogueSO",menuName ="Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;

    [Header("Conditional Requirements (Optional)")]
    public NPCso[] requiredNPCs;
    public ItemSO[] requiredItems;

    public bool IsConditionMet()
    {
        if(requiredNPCs.Length > 0)
        {
            foreach (var npc in requiredNPCs)
            {
                if (!DialogueHistoryTracker.Instance.HasSpokenWith(npc))
                {
                    Debug.Log("Is condition met returns false");
                    return false;
                }
                    
            }
        }
        Debug.Log("Is condition met return true");
        
        if(requiredItems.Length > 0)
        {
            foreach (var item in requiredItems)
            {
                if (!Inventory_Mgr.Instance.HasItem(item))
                    return false;
            }
        }

        return true;
    }



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
