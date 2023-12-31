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
    //[Inject] DialogueManager _dialogueManager;
    //[Inject] NarrationManager _narrationManager;
    [Inject] AudioManager _audioManager;

    protected override void OnEnter()
    {
        // init game stuff

        //_narrationManager.ResetNarrations();
        //_dialogueManager.ResetDialogues();
        _gameManager.StartGame();
        _audioManager.StartGameAudio();

        owningStateMachine.ToNextState();
    }

    protected override void OnExit()
    {
    }
}
