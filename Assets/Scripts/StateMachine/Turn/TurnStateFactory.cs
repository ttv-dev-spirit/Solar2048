#nullable enable
using Solar2048.StateMachine.States;

namespace Solar2048.StateMachine
{
    public sealed class TurnStateFactory
    {
        public readonly PlayCardState PlayCardState;
        public readonly BotMoveState BotMoveState;

        public TurnStateFactory(PlayCardState.Factory playCardState, BotMoveState.Factory botMoveState)
        {
            PlayCardState = playCardState.Create();
            BotMoveState = botMoveState.Create();
        }
    }
}