using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMgr : MonoBehaviour
{
    public static DialogueMgr Instance;

    [Header("UI References")]
    public Image portrait;
    public TMP_Text NPCname;
    public TMP_Text dialogueText;
    public CanvasGroup canvasGroup;
    public Button[] choiceButtons;

    public bool isDialogueActive;

    private DialogueSO curDialgoue;
    private int dialogueIndex;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }  

    }

    public void StartDialogue(DialogueSO dialogueSO)
    {
        curDialgoue = dialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;
        //ShowDialogue();
        AdvanceDialogue();
    }

    public void AdvanceDialogue()
    {
        if(dialogueIndex < curDialgoue.lines.Length)
        {
            ShowDialogue();
        }else
        {
            ShowChoices();
        }
    }

    private void ShowDialogue()
    {
        DialogueLine line = curDialgoue.lines[dialogueIndex];

        DialogueHistoryTracker.Instance.RecordNPC(line.speaker);
        
        portrait.sprite = line.speaker.portrait;
        NPCname.text = line.speaker.NPCname;

        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex++;
    }

    private void ShowChoices()
    {
        ClearChoices();

        if(curDialgoue.options.Length > 0)
        {
            for (int i = 0; i < curDialgoue.options.Length; i++)
            {
                var option = curDialgoue.options[i];
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceButtons[i].gameObject.SetActive(true);

                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue));
            }
        }else
        {
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "Nara";
            choiceButtons[0].onClick.AddListener(EndDialogue);
            choiceButtons[0].gameObject.SetActive(true);
        }
    }

    private void ChooseOption(DialogueSO dialogueSO)
    {
        if(dialogueSO == null)
        {
            EndDialogue();
        }else
        {
            ClearChoices();
            StartDialogue(dialogueSO);
        }
    }
    private void EndDialogue()
    {
        dialogueIndex = 0;
        isDialogueActive = false;
        ClearChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ClearChoices()
    {
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }

}
