using Assets.Scripts;
using CardboardCore.DI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

[Injectable]
public class AudioManager : MonoBehaviour
{
    [Header("Ref")]
    public AudioSource menuAudioSource;
    public AudioSource dialogueAudioSource;
    public AudioSource ambientAudioSource;
    public AudioSource footstepsAudioSource;
    public AudioSource menuClicksAudioSource;
    public AudioSource textAudioSource;
    public PlayerAnim playerAnim;
    public NarrationUI narrUI;

    [Header("AudioClips")]
    public List<AudioClip> footSteps;

    [Header("Control")]
    public float footstepTime;
    private float footstepTimer;
    public float textTime;
    public float textTimeSlow = 0.15f;
    public float textTimeFast = 0.11f;
    private float textTimer;

    public float baseAmbientVol;
    public float fadedAmbientVol;
    public float menuVol;
    public float dialogueVol;
    public float audioFadeDuration = 1.5f;

    // Update is called once per frame
    void Update() {
        footstepTimer += Time.deltaTime;
        textTimer += Time.deltaTime;

        if (playerAnim.isRunning && footstepTimer >= footstepTime) {
            footstepsAudioSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Count)]);
            footstepTimer = 0;
        }

        if (narrUI.isTexting && textTimer >= textTime) {
            textAudioSource.Play();
            textTimer = 0;
        }
    }

    public void StartDialogueAudio () {
        ambientAudioSource.DOFade(fadedAmbientVol, audioFadeDuration);

        if (!dialogueAudioSource.isPlaying) {
            dialogueAudioSource.volume = 0;
            dialogueAudioSource.Play();
        }

        dialogueAudioSource.DOFade(dialogueVol, audioFadeDuration);
    }

    public void EndDialogueAudio () {
        dialogueAudioSource.DOFade(0, audioFadeDuration);
        ambientAudioSource.DOFade(baseAmbientVol, audioFadeDuration);
    }

    public void StartGameAudio () {
        if (!ambientAudioSource.isPlaying) {
            ambientAudioSource.volume = 0;
            ambientAudioSource.Play();
        }

        ambientAudioSource.DOFade(baseAmbientVol, audioFadeDuration);
    }

    public void EndGameAudio() {
        if (ambientAudioSource.isPlaying) {
            ambientAudioSource.DOFade(0, audioFadeDuration);
        }
    }

    public void StartMenuAudio () {
        if (!menuAudioSource.isPlaying) {
            menuAudioSource.volume = 0;
            menuAudioSource.Play();
        }

        menuAudioSource.DOFade(menuVol, audioFadeDuration);
    }

    public void EndMenuAudio() {
        menuAudioSource.DOFade(0, audioFadeDuration);
    }

    public void PlayMenuClick () {
        menuClicksAudioSource.Play();
    }

    public void FastTextAudio () {
        textTime = textTimeFast;
    }
    public void SlowTextAudio () {
        textTime = textTimeSlow;
    }
}