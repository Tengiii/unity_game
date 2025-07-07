using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    public Rigidbody2D rb;
    public DialogueSO dialogueSO;

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
        if (Input.GetButtonDown("Interact"))
        {
            if (DialogueMgr.Instance.isDialogueActive)
            {
                DialogueMgr.Instance.AdvanceDialogue();
            }else
            {
                DialogueMgr.Instance.StartDialogue(dialogueSO); 
            }
        }
    }
}
