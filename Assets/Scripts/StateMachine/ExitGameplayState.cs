using CardboardCore.DI;
using CardboardCore.StateMachines;
using CardboardCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExitGameplayState : State
{
    [Inject] GameManager _gameManager;

    protected override void OnEnter()
    {
        // end game stuff
        _gameManager.EndGame();

        owningStateMachine.ToNextState();
    }

    protected override void OnExit()
    {
    }
}
