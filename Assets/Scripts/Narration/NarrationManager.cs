using CardboardCore.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Injectable]
public class NarrationManager : MonoBehaviour
{
    [Header("Ref")]
    public NarrationUI narratorUI;
    public List<NarratorController> narrationControllers;
    public List<string> narrationControllerKeys;
    private int currNarrController = 0;

    [Header("Data")]
    public List<NarrationInteraction> narrationInteractions;
    private List<NarrationInteraction> gameNarrationInteractions;
    private Dictionary<string, NarrationInteraction> narrationsDic;

    [Header("Events")]
    public Action InvokeNarrationEvent;
    public Action EndNarrationEvent;

    public void ResetNarrations() {
        gameNarrationInteractions = new List<NarrationInteraction>();
        narrationInteractions.ForEach(di => gameNarrationInteractions.Add(di));

        narrationsDic = new Dictionary<string, NarrationInteraction>();
        gameNarrationInteractions.ForEach(di => {
            narrationsDic.Add(di.narrationInteractionKey, di);
        });
    }

    public bool isOpeningNarration() {
        return narrationControllers[currNarrController].isOpener;
    }

    public void InvokeNarration (int index) {
        currNarrController = index;
        InvokeNarrationEvent?.Invoke();
    }

    public void InvokeEndNarration() {
        EndNarrationEvent?.Invoke();
    }

    public void StartNarration() {
        narrationControllers[currNarrController].StartNarrationInteraction(narrationsDic[narrationControllerKeys[currNarrController]]);
    }

    public void EndNarration() {
        narrationControllers[currNarrController].EndNarration();
    }
}
