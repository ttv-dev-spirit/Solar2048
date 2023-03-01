#nullable enable
using Solar2048.StateMachine.Game.States;

namespace Solar2048.StateMachine.Game
{
    public sealed class GameStateFactory
    {
        public readonly NewGameState NewGameState;
        public readonly GameRoundState GameRoundState;
        public readonly DisposeResourcesState DisposeResourcesState;
        public readonly InitializeGameState InitializeGameState;
        public readonly MainMenuState MainMenuState;

        public GameStateFactory(NewGameState.Factory newGameState, GameRoundState.Factory gameRoundState,
            DisposeResourcesState.Factory disposeResources, InitializeGameState.Factory initializeGameState,
            MainMenuState.Factory mainMenuState)
        {
            NewGameState = newGameState.Create();
            GameRoundState = gameRoundState.Create();
            DisposeResourcesState = disposeResources.Create();
            InitializeGameState = initializeGameState.Create();
            MainMenuState = mainMenuState.Create();
        }
    }
}