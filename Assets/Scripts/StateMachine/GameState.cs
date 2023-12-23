﻿using CardboardCore.DI;
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

    protected override void OnEnter()
    {
        _gameManager._pauseMenuEvent += PauseMenu;
        playerController.UnpauseCharacter();
    }

    protected override void OnExit()
    {
        _gameManager._pauseMenuEvent -= PauseMenu;
    }

    void PauseMenu () {
        owningStateMachine.ToState<PauseMenuState>();
    }
}
