using CardboardCore.DI;
using CardboardCore.StateMachines;
using TarodevController;

public class DialogueState : State
{
    [Inject] DialogueManager _dialogueManager;
    [Inject] PlayerController _playerController;

    protected override void OnEnter() {
        _dialogueManager.EndDialogueEvent += DialogueEnded;
        
        _playerController.PauseCharacter(1);
        _dialogueManager.StartDialogue();
    }

    protected override void OnExit() {
        _dialogueManager.EndDialogueEvent -= DialogueEnded;
        _dialogueManager.EndDialogue();
    }

    void DialogueEnded() {
        owningStateMachine.ToNextState();
    }
}
