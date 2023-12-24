using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Dialogue
{
    [Serializable]
    public class DialogueNode
    {
        [Header("NODE")]
        public string DialogueNodeKey;
        public string CharacterName;
        public string Text;

        [Header("EVENTS")]
        public UnityEvent NodeStartEvent;
        public UnityEvent NodeEndEvent;

        [Header("ANSWERS")]
        public List<DialogueAnswer> Answers;
    }

    [Serializable]
    public class DialogueAnswer
    {
        public string Text;
        public string NextDialogueNode;
        public bool IsEndNode;
        public UnityEvent AnswerSelectedEvent;
    }
}