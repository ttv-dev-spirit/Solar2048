#nullable enable
using Solar2048.StateMachine.States;

namespace Solar2048.StateMachine
{
    public sealed class GameStateFactory
    {
        public readonly NewGameState NewGameState;
        public readonly GameRoundState GameRoundState;
        public readonly DisposeResourcesState DisposeResourcesState;

        public GameStateFactory(NewGameState.Factory initializeGameState, GameRoundState.Factory gameRoundState, DisposeResourcesState.Factory disposeResources)
        {
            NewGameState = initializeGameState.Create();
            GameRoundState = gameRoundState.Create();
            DisposeResourcesState = disposeResources.Create();
        }
    }
}