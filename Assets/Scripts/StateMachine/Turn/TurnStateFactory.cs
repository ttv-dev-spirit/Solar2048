#nullable enable
using Solar2048.StateMachine.Turn.States;

namespace Solar2048.StateMachine.Turn
{
    public sealed class TurnStateFactory
    {
        public readonly PlayCardState PlayCardState;
        public readonly BotMoveState BotMoveState;
        public readonly GamePauseState GamePauseState;

        public TurnStateFactory(PlayCardState.Factory playCardState, BotMoveState.Factory botMoveState,
            GamePauseState.Factory gamePauseState)
        {
            PlayCardState = playCardState.Create();
            BotMoveState = botMoveState.Create();
            GamePauseState = gamePauseState.Create();
        }
    }
}