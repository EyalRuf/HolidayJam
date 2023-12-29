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
        // Menu & Opening
        SetInitialState<MenuState>();
        AddStaticTransition<MenuState, NarrationState>();
        AddFreeFlowTransition<NarrationState, InitGameState>();
        AddStaticTransition<InitGameState, GameState>();

        // Pause Menu
        AddFreeFlowTransition<GameState, PauseMenuState>();
        AddStaticTransition<PauseMenuState, GameState>();
        AddFreeFlowTransition<PauseMenuState, ExitGameplayState>();
        AddStaticTransition<ExitGameplayState, MenuState>();

        // Narration
        AddFreeFlowTransition<GameState, NarrationState>();
        AddStaticTransition<NarrationState, GameState>();
        AddFreeFlowTransition<NarrationState, DialogueState>();

        // Dialogue
        AddFreeFlowTransition<GameState, DialogueState>();
        AddStaticTransition<DialogueState, EndDialogueState>();
        AddStaticTransition<EndDialogueState, GameState>();

        Start();
    }
}
