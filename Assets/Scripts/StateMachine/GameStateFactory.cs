#nullable enable
using Solar2048.StateMachine.States;

namespace Solar2048.StateMachine
{
    public sealed class GameStateFactory
    {
        public readonly InitializeGameState InitializeGameState;
        public readonly GameRoundState GameRoundState;

        public GameStateFactory(InitializeGameState.Factory initializeGameState, GameRoundState.Factory gameRoundState)
        {
            InitializeGameState = initializeGameState.Create();
            GameRoundState = gameRoundState.Create();
        }
    }
}