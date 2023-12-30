using CardboardCore.DI;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

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
    public Light2D l;

    void Start() {
        IntensityBreath1();
    }

    void IntensityBreath1 () {
        float intensity = 0.9f;

        DOTween.To(() => intensity, (x) => intensity = x, 0.6f, 2f).OnUpdate(() => {
            l.intensity = intensity;
        }).OnComplete(() => IntensityBreath2());
    }
    void IntensityBreath2() {
        float intensity = 0.6f;

        DOTween.To(() => intensity, (x) => intensity = x, 0.9f, 2f).OnUpdate(() => {
            l.intensity = intensity;
        }).OnComplete(() => IntensityBreath1());
    }

    void Update() {
        if (!player.isInteracting &&
                Vector2.Distance(player.transform.position, interactionTransform.position) <= interactionDistance &&
                Input.GetKeyDown(KeyCode.Return)) {

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

        currDialogueInteraction.currentDialogueNodeKey = answer.nextDialogueNode;
        if (!answer.doesEndDialogue) {
            StartNode();
        } else {
            InvokeEndDialogueInteraction();
        }
    }
}
