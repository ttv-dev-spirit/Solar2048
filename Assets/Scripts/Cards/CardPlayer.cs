#nullable enable
using System;
using Solar2048.Buildings;
using Solar2048.Map;
using UniRx;

namespace Solar2048.Cards
{
    public sealed class CardPlayer : IActivatable, ICardPlayer
    {
        private readonly Subject<Unit> _onCardPlayed = new();
        private readonly Hand _hand;
        private readonly BuildingsManager _buildingsManager;
        private readonly GameMap _gameMap;

        public bool IsActive { get; private set; }
        public IObservable<Unit> OnCardPlayed => _onCardPlayed;

        public CardPlayer(Hand hand, BuildingsManager buildingsManager, GameMap gameMap)
        {
            _hand = hand;
            _buildingsManager = buildingsManager;
            _gameMap = gameMap;
            IsActive = false;
        }

        public void PlayCardFromHandTo(Card card, Field field)
        {
            if (!IsActive)
            {
                return;
            }
            
            _hand.RemoveCard(card);
            _buildingsManager.AddNewBuilding(card.BuildingType, field);
            _gameMap.RecalculateStats();
            _onCardPlayed.OnNext(Unit.Default);
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}