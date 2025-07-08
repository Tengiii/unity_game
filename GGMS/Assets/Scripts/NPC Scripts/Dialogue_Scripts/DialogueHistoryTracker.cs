using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHistoryTracker : MonoBehaviour
{
    public static DialogueHistoryTracker Instance;
    private readonly List<NPCso> spokenNPCs = new List<NPCso>();


    private void Awake()
    {
        if(Instance !=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /*public void RecordNPC(NPCso npcSO)
    {
        spokenNPCs.Add(npcSO);
        Debug.Log("Just spoke to "+ npcSO.NPCname);
    }*/
    public void RecordNPC(NPCso npcSO)
    {
        if (!spokenNPCs.Contains(npcSO))
        {
            spokenNPCs.Add(npcSO);
            Debug.Log(" Dodano NPC do historii: " + npcSO.NPCname);
        }
    }


    public bool HasSpokenWith(NPCso npcSO)
    {
        return spokenNPCs.Contains(npcSO); 
    }
}
