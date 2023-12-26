﻿using CardboardCore.DI;
using CardboardCore.StateMachines;
using CardboardCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InitGameState : State
{
    [Inject] GameManager _gameManager;
    [Inject] DialogueManager _dialogueManager;

    protected override void OnEnter()
    {
        // init game stuff

        _dialogueManager.ResetDialogues();
        _gameManager.StartGame();
        owningStateMachine.ToNextState();
    }

    protected override void OnExit()
    {
    }
}
