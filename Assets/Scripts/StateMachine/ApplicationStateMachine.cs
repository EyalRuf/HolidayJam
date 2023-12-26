using CardboardCore.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ApplicationStateMachine : StateMachine
{
    public ApplicationStateMachine(bool enableDebugging) : base(enableDebugging)
    {
        // Menu
        SetInitialState<MenuState>();
        AddStaticTransition<MenuState, InitGameState>();
        AddStaticTransition<InitGameState, GameState>();

        // Pause Menu
        AddFreeFlowTransition<GameState, PauseMenuState>();
        AddStaticTransition<PauseMenuState, GameState>();
        AddFreeFlowTransition<PauseMenuState, ExitGameplayState>();
        AddStaticTransition<ExitGameplayState, MenuState>();

        // Dialogue
        AddFreeFlowTransition<GameState, DialogueState>();
        AddStaticTransition<DialogueState, EndDialogueState>();
        AddStaticTransition<EndDialogueState, GameState>();

        Start();
    }
}
