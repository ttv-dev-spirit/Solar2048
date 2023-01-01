#nullable enable
using Solar2048.Buildings;
using UniRx;
using UnityEngine;

namespace Solar2048
{
    public sealed class BuildingPlacer
    {
        private readonly GameMap _gameMap;
        private readonly Hand _hand;
        private readonly BuildingsManager _buildingsManager;
        private readonly CheatHand _cheatHand;

        public BuildingPlacer(GameMap gameMap, Hand hand, BuildingsManager buildingsManager, CheatHand cheatHand)
        {
            _gameMap = gameMap;
            _hand = hand;
            _buildingsManager = buildingsManager;
            _cheatHand = cheatHand;
            _gameMap.OnFieldClicked.Subscribe(FieldClickedHandler);
        }

        private void FieldClickedHandler(Vector2Int position)
        {
            var isCheatCard = false;
            Card? selectedCard = _hand.SelectedCard.Value;
            if (selectedCard == null)
            {
                selectedCard = _cheatHand.SelectedCard;
                isCheatCard = true;
                if (selectedCard == null)
                {
                    return;
                }
            }

            Field field = _gameMap.GetField(position);
            if (field.Building != null)
            {
                return;
            }

            _buildingsManager.AddNewBuilding(selectedCard.BuildingType, field);
            if (!isCheatCard)
            {
                _hand.RemoveCard(selectedCard);
            }
            _gameMap.RecalculateStats();
        }
    }
}