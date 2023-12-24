using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 0)]
    public class DialogueInteraction : ScriptableObject
    {
        [Header("INTERACTION")]
        public string InteractionKey;

        [Header("NODES")]
        public List<DialogueNode> DialogueNodes;

        [Header("EVENTS")]
        public UnityEvent InteractionStartEvent;
        public UnityEvent InteractionEndEvent;
    }
}