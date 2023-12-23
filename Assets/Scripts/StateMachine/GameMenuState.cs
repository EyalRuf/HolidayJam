using CardboardCore.DI;
using CardboardCore.StateMachines;
using CardboardCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameMenuState : State
{
    [Inject] GameMenuManager _gameMenuManager;

    protected override void OnEnter()
    {
        _gameMenuManager.StartBtnEvent += StartGame;
        _gameMenuManager.SetMenuVisible(true);
    }

    protected override void OnExit()
    {
        _gameMenuManagergame.StartBtnEvent -= StartGame;
        
        //fade?
        _gameMenuManager.SetMenuVisible(false);
    }

    void ContinueGame() {
        owningStateMachine.ToNextState();
    }
}
