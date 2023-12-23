using CardboardCore.DI;
using CardboardCore.StateMachines;
using CardboardCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameState : State
{
    [Inject] GameManager _gameManager;

    protected override void OnEnter()
    {
        _gameManager._exitGameplayEvent += ExitGameplay;
    }

    protected override void OnExit()
    {
        _gameManager._exitGameplayEvent -= ExitGameplay;
    }

    void ExitGameplay () {

    }
}
