using CardboardCore.DI;
using CardboardCore.StateMachines;
using CardboardCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarodevController;

public class GameState : State
{
    [Inject] GameManager _gameManager;
    [Inject] PlayerController playerController;
    [Inject] DialogueManager _dialogueManager;

    protected override void OnEnter() {
        _gameManager._pauseMenuEvent += PauseMenu;
        _dialogueManager.InvokeDialogueEvent += EngageDialogue;
        playerController.UnpauseCharacter();
    }

    protected override void OnExit() {
        _gameManager._pauseMenuEvent -= PauseMenu;
        _dialogueManager.InvokeDialogueEvent -= EngageDialogue;
    }

    void PauseMenu() {
        owningStateMachine.ToState<PauseMenuState>();
    }

    void EngageDialogue() {
        owningStateMachine.ToState<DialogueState>();
    }
}
