using CardboardCore.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.Events;

public class DialogueInteractionController : MonoBehaviour {
    [Header("REFS")]
    public DialogueManager dialogueManager;
    public DialogueInteractionUI dialogueUI;
    public PlayerController player;

    [Header("INTERACTION")]
    public int dialogueControllerIndex;
    public DialogueInteraction currDialogueInteraction;
    public Transform interactionTransform;
    public float interactionDistance = 10f;

    void Update() {
        if (!player.isInteracting &&
                Vector2.Distance(player.transform.position, interactionTransform.position) <= interactionDistance &&
                Input.GetKeyDown(KeyCode.Space)) {

            dialogueManager.InvokeDialogueInteraction(dialogueControllerIndex);
        }
    }

    public void StartDialogueInteraction(DialogueInteraction dialogueInteraction) {
        currDialogueInteraction = dialogueInteraction;

        dialogueUI.StartInteraction(currDialogueInteraction);
        dialogueUI.AnswerChosenEvent += AnswerChosen;

        player.isInteracting = true;
    }

    public void CloseDialogue () {
        dialogueUI.EndInteraction();
        player.isInteracting = false;
    }

    public void InvokeEndDialogueInteraction () {
        dialogueUI.AnswerChosenEvent -= AnswerChosen;
        dialogueManager.InvokeEndDialogue();
    }

    public void StartNode() {
        dialogueUI.SetDialogueNode(currDialogueInteraction.GetCurrentDialogueNode());
    }

    public void EndNode() {
    }

    public void AnswerChosen(int answerIndex) {
        DialogueNode node = currDialogueInteraction.GetCurrentDialogueNode();
        DialogueAnswer answer = node.answers[answerIndex];

        // DO MORE THINGS AFTER ANSWER
        EndNode();

        if (!answer.doesEndDialogue) {
            currDialogueInteraction.currentDialogueNodeKey = answer.nextDialogueNode;
            StartNode();
        } else {
            InvokeEndDialogueInteraction();
        }
    }
}
