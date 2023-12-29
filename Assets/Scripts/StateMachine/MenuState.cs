using CardboardCore.DI;
using CardboardCore.StateMachines;
using CardboardCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MenuState : State
{
    [Inject] MenuManager _menuManager;

    [Inject] DialogueManager _dialogueManager;
    [Inject] NarrationManager _narrationManager;

    protected override void OnEnter()
    {
        _narrationManager.ResetNarrations();
        _dialogueManager.ResetDialogues();

        _menuManager.StartBtnEvent += StartGame;
        _menuManager.SetMenuVisible(true);
    }

    protected override void OnExit()
    {
        _menuManager.StartBtnEvent -= StartGame;
        
        //fade?
        _menuManager.SetMenuVisible(false);
    }

    void StartGame() {
        owningStateMachine.ToNextState();
    }
}
