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

        // Monologue
        //AddFreeFlowTransition<GameState, MonologueState>();
        //AddStaticTransition<MonologueState, GameState>();

        // Player Halt
        //AddFreeFlowTransition<GameState, PlayerHaltState>();
        //AddFreeFlowTransition<PlayerHaltState, DialogueState>();
        //AddStaticTransition<DialogueState, EndPlayerHaltState>();
        //AddStaticTransition<EndPlayerHaltState, GameState>();

        // Exit
        AddStaticTransition<GameState, MenuState>();

        // GameMenu
        AddFreeFlowTransition<GameState, GameMenuState>();
        AddFreeFlowTransition<GameMenuState, MenuState>();
        AddStaticTransition<GameMenuState, GameState>();

        Start();
    }
}
