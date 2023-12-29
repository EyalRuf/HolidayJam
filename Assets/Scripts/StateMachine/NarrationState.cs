using CardboardCore.DI;
using CardboardCore.StateMachines;
using TarodevController;

public class NarrationState : State
{
    [Inject] NarrationManager _narrationManager;
    [Inject] PlayerController _playerController;

    protected override void OnEnter() {
        _narrationManager.EndNarrationEvent += NarrationEnded;

        _playerController.PauseCharacter(1);
        _narrationManager.StartNarration();
    }

    protected override void OnExit() {
        _narrationManager.EndNarrationEvent -= NarrationEnded;
        _narrationManager.EndNarration();
    }

    void NarrationEnded() {
        if (_narrationManager.isOpeningNarration())
            owningStateMachine.ToState<InitGameState>();
        else
            owningStateMachine.ToNextState();
    }
}
