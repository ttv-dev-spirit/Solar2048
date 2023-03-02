#nullable enable
using Solar2048.Buildings;
using Solar2048.Cards;
using Solar2048.Cheats;
using Solar2048.StaticData;
using Zenject;

namespace Solar2048.StateMachine.Game.States
{
    public sealed class NewGameState : State
    {
        private IGameStateReseter _gameStateReseter;
        private CheatsContainer _cheatsContainer;
        private CardSpawner _cardSpawner;
        private StaticDataProvider _staticDataProvider;

        public NewGameState(IGameStateReseter gameStateReseter, CheatsContainer cheatsContainer,
            CardSpawner cardSpawner, StaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
            _cardSpawner = cardSpawner;
            _cheatsContainer = cheatsContainer;
            _gameStateReseter = gameStateReseter;
        }

        protected override void OnEnter()
        {
            _gameStateReseter.Reset();
            _cheatsContainer.Reset();
            AddStartingCardsToHand();
            Finish();
        }

        protected override void OnExit()
        {
        }

        private void AddStartingCardsToHand()
        {
            foreach (BuildingType buildingType in _staticDataProvider.GetStartingHand())
            {
                _cardSpawner.AddCardToHand(buildingType);
            }
        }

        public class Factory : PlaceholderFactory<NewGameState>
        {
        }
    }
}