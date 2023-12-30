using CardboardCore.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.Events;

public class NarratorController : MonoBehaviour
{
    [Header("REFS")]
    public NarrationManager narrationManager;
    public NarrationUI narrationUI;
    public PlayerController player;

    [Header("INTERACTION")]
    public int narrControllerIndex;
    public NarrationInteraction currNarrationInteraction;
    public bool isOpener;
    public Transform interactionTransform;
    public float interactionDistance = 3f;

    void Update() {
        // When invoke narration?
        if (isOpener) {
            return;
        }

        if (!currNarrationInteraction.IsDone() && !player.isInteracting &&
            Vector2.Distance(player.transform.position, interactionTransform.position) <= interactionDistance) {

            narrationManager.InvokeNarration(narrControllerIndex);
        }
    }

    public void StartNarrationInteraction(NarrationInteraction narrationInteraction) {
        currNarrationInteraction = narrationInteraction;

        narrationUI.StartNarration(narrationInteraction);
        narrationUI.NarrationOver += InvokeEndNarration;

        player.isInteracting = true;
    }

    public void EndNarration() {
        narrationUI.EndNarration();
        player.isInteracting = false;
    }

    public void InvokeEndNarration() {
        narrationUI.NarrationOver -= InvokeEndNarration;
        narrationManager.InvokeEndNarration();
    }
}
