using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueInteractionUI : MonoBehaviour {

    [Header("general")]
    public GameObject dialogueUIPanel;
    public TextMeshProUGUI dialogueCharacterNameTextObj;
    public TextMeshProUGUI dialogueTextObj;

    [Header("answers")]
    public List<GameObject> answerObjs;
    public List<TextMeshProUGUI> answerTexts;
    public List<GameObject> answerLines;

    public Action<int> answerChosenEvent;

    private void ToggleUI (bool flag) {
        dialogueUIPanel.SetActive(flag);
    }

    public void StartInteraction (DialogueInteraction interaction) {
        ToggleUI(true);

        SetDialogueNode(interaction.GetCurrentDialogueNode());
    }

    public void EndInteraction () {
        ToggleUI(false);
    }

    public void SetDialogueNode(DialogueNode dialogneNode) {
        dialogueCharacterNameTextObj.text = dialogneNode.characterName;
        dialogueTextObj.text = dialogneNode.text;

        answerObjs.ForEach(x => x.SetActive(false));
        answerLines.ForEach(x => x.SetActive(false));

        var index = 0;
        dialogneNode.answers.ForEach(currAnswer => {
            answerObjs[index].SetActive(true);
            answerTexts[index].text = currAnswer.text;

            index++;
        });
    }

    public void OnAnswerHovered (int answerIndex) {
        answerLines[answerIndex].SetActive(true);
    }

    public void OnAnswerUnhovered(int answerIndex) {
        answerLines[answerIndex].SetActive(false);
    }

    public void OnAnswerSelected(int answerIndex) {
        answerChosenEvent?.Invoke(answerIndex);
    }
}
