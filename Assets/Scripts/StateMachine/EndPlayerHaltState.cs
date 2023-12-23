using CardboardCore.DI;
using CardboardCore.StateMachines;
using CardboardCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EndPlayerHaltState : State
{
    //[Inject] MenuManager _menuManager;

    protected override void OnEnter()
    {
        //_menuManager.StartBtnEvent += StartRun;
        //_menuManager.SetMenuVisible(true);
    }

    protected override void OnExit()
    {
        //_menuManager.StartBtnEvent -= StartRun;
        //_menuManager.SetMenuVisible(false);
    }

    //void StartRun ()
    //{
    //    owningStateMachine.ToNextState();
    //}
}
