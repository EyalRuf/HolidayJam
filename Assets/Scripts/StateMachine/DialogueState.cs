using CardboardCore.DI;
using CardboardCore.StateMachines;
using TarodevController;

public class DialogueState : State
{
    [Inject] DialogueManager _dialogueManager;
    [Inject] PlayerController _playerController;
    [Inject] AudioManager _audioManager;

    protected override void OnEnter() {
        _dialogueManager.EndDialogueEvent += DialogueEnded;
        
        _playerController.PauseCharacter(1);
        _dialogueManager.StartDialogue();

        _audioManager.StartDialogueAudio();
    }

    protected override void OnExit() {
        _dialogueManager.EndDialogueEvent -= DialogueEnded;
        _dialogueManager.EndDialogue();
    }

    void DialogueEnded() {
        owningStateMachine.ToNextState();
    }
}
