using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueInteraction
{
    [Header("INTERACTION")]
    public string interactionKey;
    public string currentDialogueNodeKey;

    [Header("NODES")]
    public List<DialogueNode> dialogueNodes;

    public DialogueNode GetCurrentDialogueNode() {
        var currNode = dialogueNodes.Find(curr => curr.dialogueNodeKey == currentDialogueNodeKey);

        if (currNode == null)
            Debug.LogError("MISSING DIALOGUE NODE IN INTERACTION: " + interactionKey + ", CURR_NODE: " + currentDialogueNodeKey);

        return currNode;
    }

    public DialogueInteraction (DialogueInteraction di) {
        interactionKey = di.interactionKey;
        currentDialogueNodeKey = di.currentDialogueNodeKey;

        dialogueNodes = new List<DialogueNode>();
        di.dialogueNodes.ForEach(dn => dialogueNodes.Add(new DialogueNode(dn)));
    }
}

[Serializable]
public class DialogueNode
{
    [Header("NODE")]
    public string dialogueNodeKey;
    public string characterName;
    public string text;

    [Header("ANSWERS")]
    public List<DialogueAnswer> answers;

    public DialogueNode (DialogueNode dn) {
        dialogueNodeKey = dn.dialogueNodeKey;
        characterName = dn.characterName;
        text = dn.text;

        answers = new List<DialogueAnswer>();
        dn.answers.ForEach(ans => answers.Add(new DialogueAnswer(ans)));
    }
}

[Serializable]
public class DialogueAnswer
{
    public string text;
    public string nextDialogueNode;
    public bool doesEndDialogue;
    public DialogueAnswerTrigger answerTrigger;

    public DialogueAnswer(DialogueAnswer da) {
        text = da.text;
        nextDialogueNode = da.nextDialogueNode;
        doesEndDialogue = da.doesEndDialogue;
        answerTrigger = da.answerTrigger;
    }
}

public enum DialogueAnswerTrigger
{
    none,
}
