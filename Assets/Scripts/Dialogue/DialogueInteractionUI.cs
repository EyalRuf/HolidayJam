using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueInteractionUI : MonoBehaviour {
    [Header("ref")]
    public AudioManager audioManager;

    [Header("general")]
    public GameObject dialogueUIOutsidePanel;
    public GameObject dialogueUIInsidePanel;
    public TextMeshProUGUI dialogueCharacterNameTextObj;
    public TextMeshProUGUI dialogueTextObj;
    public bool isOn;

    [Header("answers")]
    public List<GameObject> answerObjs;
    public List<TextMeshProUGUI> answerTexts;
    public List<GameObject> answerLines;

    public Action<int> AnswerChosenEvent;

    [Header("dialogue-steps")]
    public DialogueStep currDialogueStep;
    public float fadeDuration = 1f;
    public float textSpeed = 0.1f;
    public float currTextProgress = 0;

    void Update() {
        //if (isOn) {
        //    switch (currDialogueStep) {
        //        case DialogueStep.FadeIn: {
        //                break;
        //            }
        //        case DialogueStep.Text: {
        //                break;
        //            }
        //        case DialogueStep.Answers: {
        //                break;
        //            }
        //        case DialogueStep.FadeOut: {
        //                break;
        //            }
        //        default: {
        //                break;
        //            }
        //    }
        //}
    }

    private void ToggleUI (bool flag) {
        isOn = flag;
        
        dialogueUIOutsidePanel.SetActive(flag);
    }

    public void StartInteraction (DialogueInteraction interaction) {
        ToggleUI(true);
        // Fade UI in

        Sequence seq = DOTween.Sequence();
        //seq.c

        SetDialogueNode(interaction.GetCurrentDialogueNode());
    }
    public void EndInteraction () {
        // Fade UI out
        ToggleUI(false);
    }

    public void SetDialogueNode(DialogueNode dialogneNode) {
        dialogueCharacterNameTextObj.text = dialogneNode.characterName;
        dialogueTextObj.text = dialogneNode.text;

        answerObjs.ForEach(x => x.SetActive(false));
        answerLines.ForEach(x => x.SetActive(false));

        currDialogueStep = 0;
        currTextProgress = 0;

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
        AnswerChosenEvent?.Invoke(answerIndex);
        audioManager.PlayMenuClick();
    }
}
public enum DialogueStep
{
    FadeIn = 0,
    Text = 1,
    Answers = 2,
    FadeOut = 3,
}
