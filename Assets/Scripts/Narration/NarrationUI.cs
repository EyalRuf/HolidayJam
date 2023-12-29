using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NarrationUI : MonoBehaviour
{
    [Header("REF")]
    public GameObject narrationUIPanelObject;
    public List<Image> imagesUI;
    public TextMeshProUGUI narrationTextObj;
    public TextMeshProUGUI skipTextObj;

    [Header("NARRATION")]
    private NarrationInteraction narratorInteraction;

    [Header("CTRL")]
    public bool isOn = false;
    public float textSpeed = 1;
    public float fadeSpeed = 2;
    public bool canNextText = false;
    public float imageAlpha = 0.1f;

    Sequence fadeInSequence;
    Sequence textSequence;
    Sequence fadeOutSequence;
    Sequence parentSequence;

    [Header("EVENTS")]
    public Action NarrationOver;

    void Start() {
        DOTween.defaultEaseType = Ease.Linear;
    }

    void Update() {
        if (!isOn) return;

        var isSkipBtnDown = Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space);
        var isSkipBtn = Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space);

        if (canNextText) {
            if (isSkipBtnDown) {
                ClickedNextTextNode();
            }
        } else {
            if (isSkipBtnDown) {
                parentSequence.timeScale = 3;
            }
        }
    }

    public void StartNarration(NarrationInteraction narratorInteraction) {
        this.narratorInteraction = narratorInteraction;
        
        narrationUIPanelObject.SetActive(true);
        
        narrationTextObj.text = "";
        narrationTextObj.color = new Color(1, 1, 1, 1);
        skipTextObj.color = new Color(1, 1, 1, 0);
        isOn = true;

        FadeInSequence();
    }

    public void InvokeEndNarration() {
        NarrationOver?.Invoke();
    }

    public void EndNarration () {
        narrationUIPanelObject.SetActive(false);
    }

    private void ClickedNextTextNode() {
        if (parentSequence.active) {
            parentSequence.Complete(true);
        } 
        else {

            if (!narratorInteraction.IsDone()) {
                SetTextSequence();
                skipTextObj.color = new Color(1, 1, 1, 0);
            } else {
                SetFadeOutSequence();
            }
        }
    }

    void ResetSequences () {
        if (fadeInSequence != null) {
            fadeInSequence.Kill();
        }
        fadeInSequence = DOTween.Sequence();

        if (textSequence != null) {
            textSequence.Kill();
        }
        textSequence = DOTween.Sequence();

        if (fadeOutSequence != null) {
            fadeOutSequence.Kill();
        }
        fadeOutSequence = DOTween.Sequence();

        if (parentSequence != null) {
            parentSequence.Kill();
        }
    }

    void FadeInSequence () {
        ResetSequences();

        fadeInSequence.Append(FadeInUISequence());

        parentSequence = DOTween.Sequence().Append(fadeInSequence).OnComplete(() => SetTextSequence());
        parentSequence.Play();
    }
    
    void SetTextSequence () {
        ResetSequences();

        textSequence.Append(TextSequence());
        canNextText = false;

        parentSequence = DOTween.Sequence().Append(textSequence).OnComplete(() => {

            canNextText = true;
            narratorInteraction.currNodeIndex++;

            skipTextObj.DOColor(new Color(1, 1, 1, 0.5f), 3f).SetDelay(3f);
        });
    }

    void SetFadeOutSequence() {
        ResetSequences();

        fadeOutSequence.Append(FadeOutSequence());
        parentSequence = DOTween.Sequence().Append(fadeOutSequence).OnComplete(() => {
            InvokeEndNarration();
        });
    }

    Sequence FadeInUISequence() {
        Sequence fadeInSequence = DOTween.Sequence();

        imagesUI.ForEach(image => fadeInSequence.Join(image.DOFade(imageAlpha, fadeSpeed)));

        return fadeInSequence;
    }

    Tween TextSequence () {
        string typeWriterText = "";
        string nodeText = narratorInteraction.GetCurrentText();

        return DOTween.To(() => typeWriterText, (x) => typeWriterText = x, nodeText, textSpeed).OnUpdate(() => {
            narrationTextObj.text = typeWriterText;
        });
    }

    Sequence FadeOutSequence () {
        Sequence fadeOutSequence = DOTween.Sequence();

        fadeOutSequence.Join(skipTextObj.DOColor(new Color(1, 1, 1, 0), fadeSpeed));
        fadeOutSequence.Join(narrationTextObj.DOColor(new Color(1, 1, 1, 0), fadeSpeed));
        imagesUI.ForEach(image => fadeOutSequence.Join(image.DOFade(0, fadeSpeed)));

        return fadeOutSequence;
    }
}