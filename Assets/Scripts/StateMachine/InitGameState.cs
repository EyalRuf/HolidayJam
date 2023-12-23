using CardboardCore.DI;
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

    protected override void OnEnter()
    {
        owningStateMachine.ToNextState();
    }

    protected override void OnExit()
    {
        _gameManager.StartGame();
    }
}
