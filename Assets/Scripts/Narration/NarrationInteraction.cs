using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class NarrationInteraction
{
    [Header("INTERACTION")]
    public string narrationInteractionKey;
    public int currNodeIndex = 0;

    [Header("NODES")]
    public List<string> narrationTexts;

    public string GetCurrentText() {
        if (currNodeIndex > narrationTexts.Count) {
            return "";
        }

        return narrationTexts[currNodeIndex];
    }

    public bool IsDone() {
        return currNodeIndex >= narrationTexts.Count;
    }

    public NarrationInteraction (NarrationInteraction ni) {
        narrationInteractionKey = ni.narrationInteractionKey;
        currNodeIndex = 0;
        narrationTexts = ni.narrationTexts;
    }
}
