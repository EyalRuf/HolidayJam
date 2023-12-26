using CardboardCore.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Injectable]
public class DialogueManager : MonoBehaviour {

    [Header("Refs")]
    public DialogueInteractionUI dialogueUI;
    public List<DialogueInteractionController> interactionControllers;
    public List<string> interactionControllerKeys;

    [Header("Data")]
    public List<DialogueInteraction> dialogueInteractionList;
    private List<DialogueInteraction> gameDialogueInteractionList;
    private Dictionary<string, DialogueInteraction> dialogueInteractionsDic;
    private int currIntController = 0;

    [Header("Events")]
    public Action InvokeDialogueEvent;
    public Action EndDialogueEvent;

    public void ResetDialogues () {
        gameDialogueInteractionList = new List<DialogueInteraction>();
        dialogueInteractionList.ForEach(di => gameDialogueInteractionList.Add(di));

        dialogueInteractionsDic = new Dictionary<string, DialogueInteraction>();
        gameDialogueInteractionList.ForEach(di => {
            dialogueInteractionsDic.Add(di.interactionKey, di);
        });
    }

    public void InvokeDialogueInteraction(int index) {
        currIntController = index;
        InvokeDialogueEvent?.Invoke();
    }

    public void InvokeEndDialogue () {
        EndDialogueEvent?.Invoke();
    }

    public void StartDialogue () {
        interactionControllers[currIntController].StartDialogueInteraction(dialogueInteractionsDic[interactionControllerKeys[currIntController]]);
    }

    public void EndDialogue() {
        interactionControllers[currIntController].CloseDialogue();
    }
}
