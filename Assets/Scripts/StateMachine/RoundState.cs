#nullable enable
using Solar2048.Buildings;

namespace Solar2048.StateMachine
{
    public sealed class RoundState : State
    {
        private readonly CardSpawner _cardSpawner;

        public RoundState(CardSpawner cardSpawner)
        {
            _cardSpawner = cardSpawner;
        }

        protected override void OnEnter()
        {
            _cardSpawner.AddCardToHand(BuildingType.WindTurbine);
            _cardSpawner.AddCardToHand(BuildingType.WindTurbine);
            _cardSpawner.AddCardToHand(BuildingType.WindTurbine);
            _cardSpawner.AddCardToHand(BuildingType.WindTurbine);
        }

        protected override void OnExit()
        {
        }
    }
}