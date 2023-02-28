#nullable enable

using Solar2048.Buildings;
using Solar2048.Cards;
using Solar2048.StateMachine.Messages;
using Solar2048.StateMachine.States;
using UniRx;
using Zenject;

namespace Solar2048.StateMachine.Cheats
{
    public sealed class CheatCardsSupplier : IActivatable
    {
        private readonly CardSpawner _cardSpawner;

        public bool IsActive { get; private set; }

        public CheatCardsSupplier(CardSpawner cardSpawner, IMessageReceiver messageReceiver)
        {
            _cardSpawner = cardSpawner;
            messageReceiver.Receive<NewGameMessage>().Subscribe(OnNewGame);
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        private void OnNewGame(NewGameMessage _)
        {
            _cardSpawner.AddCardToHand(BuildingType.SolarPanel);
            _cardSpawner.AddCardToHand(BuildingType.SolarPanel);
            _cardSpawner.AddCardToHand(BuildingType.SolarPanel);
            _cardSpawner.AddCardToHand(BuildingType.SolarPanel);
        }

        public class Factory : PlaceholderFactory<CheatCardsSupplier>
        {
        }
    }
}