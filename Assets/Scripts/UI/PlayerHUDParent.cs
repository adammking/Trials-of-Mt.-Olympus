using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDParent : MonoBehaviour
{

    public GameObject dialogueWindow;
    public GameObject abilitiesWindow;

    public delegate void ShowAbilitiesWindow();
    public static ShowAbilitiesWindow toggleAbilitiesWindow;

    public delegate void ShowDialogueWindow();
    public static ShowDialogueWindow toggleDialogueWindow;

    private CanvasGroup abilitiesCanvasGroup;
    private CanvasGroup dialogueCanvasGroup;

    private void Awake()
    {
        
        abilitiesCanvasGroup = abilitiesWindow.GetComponent<CanvasGroup>();
        dialogueCanvasGroup = dialogueWindow.GetComponent<CanvasGroup>();

    }

    private void OnEnable()
    {
        toggleAbilitiesWindow += ToggleAbilitiesUI;
        toggleDialogueWindow += ToggleDialogueUI;
    }

    private void OnDisable()
    {
        toggleAbilitiesWindow -= ToggleAbilitiesUI;
        toggleDialogueWindow -= ToggleDialogueUI;
    }

    private void ToggleAbilitiesUI()
    {

        if (abilitiesCanvasGroup.alpha == 0)
        {
            abilitiesCanvasGroup.alpha = 1;
            abilitiesCanvasGroup.blocksRaycasts = true;
            abilitiesCanvasGroup.interactable = true;
        } else
        {
            abilitiesCanvasGroup.alpha = 0;
            abilitiesCanvasGroup.blocksRaycasts = false;
            abilitiesCanvasGroup.interactable = false;
        }

    }

    private void ToggleDialogueUI()
    {

        if (dialogueCanvasGroup.alpha == 0)
        {
            dialogueCanvasGroup.alpha = 1;
            dialogueCanvasGroup.blocksRaycasts = true;
            dialogueCanvasGroup.interactable = true;
        }
        else
        {
            dialogueCanvasGroup.alpha = 0;
            dialogueCanvasGroup.blocksRaycasts = false;
            dialogueCanvasGroup.interactable = false;
        }

    }

}
