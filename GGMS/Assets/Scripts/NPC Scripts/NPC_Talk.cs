using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    public Rigidbody2D rb;
    public List<DialogueSO> conversations;
    public DialogueSO curConversation;


    private void OnEnable()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        
    }
    private void OnDisable()
    {
        rb.isKinematic = false;
    }

    private void Update()
    {
        /*if (Input.GetButtonDown("Talk"))
        {
            if (DialogueMgr.Instance.isDialogueActive)
            {
                DialogueMgr.Instance.AdvanceDialogue();
            }else
            {
                CheckForNewConversation();
                DialogueMgr.Instance.StartDialogue(curConversation); 
            }
        }*/
        if (Input.GetButtonDown("Talk"))
        {
            if (DialogueMgr.Instance.isDialogueActive)
            {
                DialogueMgr.Instance.AdvanceDialogue();
            }
            else
            {
                CheckForNewConversation();
                if (curConversation != null)
                {
                    DialogueMgr.Instance.StartDialogue(curConversation);
                }
                else
                {
                    Debug.Log(" Brak rozmowy do rozpoczêcia");
                }
            }
        }
    }

    /*private void CheckForNewCOnversation()
    {
        for(int i = conversations.Count - 1; i >= 0; i--)
        {
            var convo = conversations[i];
            if(convo != null && convo.IsConditionMet())
            {
                conversations.RemoveAt(i);
                curConversation = convo;
                Debug.Log("CUR CONOV JEST KURWA CUR ");
            }
        }  
    }*/
    private void CheckForNewConversation()
    {
        //for (int i = conversations.Count - 1; i >= 0; i--)
        for(int i = 0; i < conversations.Count; i++)
        {
            var convo = conversations[i];
            if (convo != null && convo.IsConditionMet())
            {
                curConversation = convo;
                conversations.RemoveAt(i);
                Debug.Log("Wybrano rozmowê: " + convo.name);
                return; // Zatrzymujemy siê po pierwszym dopasowaniu
            }
        }

        Debug.LogWarning("Brak rozmów spe³niaj¹cych warunki");
    }
}
