using CardboardCore.DI;
using CardboardCore.StateMachines;
using CardboardCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarodevController;

public class EndDialogueState : State
{
    //[Inject] DialogueManager _dialogueManager;
    //[Inject] PlayerController _playerController;
    [Inject] AudioManager _audioManager;

    protected override void OnEnter()
    {
        _audioManager.EndDialogueAudio();
        owningStateMachine.ToNextState();
    }

    protected override void OnExit()
    {
    }
}
