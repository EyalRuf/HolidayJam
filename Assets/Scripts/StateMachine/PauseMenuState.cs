using CardboardCore.DI;
using CardboardCore.StateMachines;
using CardboardCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarodevController;

[Injectable]
public class PauseMenuState : State
{
    [Inject] PauseMenuManager _pauseMenuManager;
    [Inject] PlayerController playerController;

    protected override void OnEnter()
    {
        _pauseMenuManager.ContinueBtnEvent += ContinueGame;
        _pauseMenuManager.ExitBtnEvent += ExitToMenu;
        _pauseMenuManager.SetPauseMenuVisible(true);

        playerController.PauseCharacter(2);
    }

    protected override void OnExit()
    {
        _pauseMenuManager.ContinueBtnEvent -= ContinueGame;
        _pauseMenuManager.ExitBtnEvent -= ExitToMenu;
        _pauseMenuManager.SetPauseMenuVisible(false);
    }

    void ContinueGame() {
        owningStateMachine.ToNextState();
    }

    void ExitToMenu ()
    {
        owningStateMachine.ToState<ExitGameplayState>();
    }
}
