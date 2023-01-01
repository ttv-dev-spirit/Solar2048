#nullable enable
using Solar2048.Buildings;
using UniRx;
using UnityEngine;

namespace Solar2048
{
    public sealed class BuildingPlacer
    {
        private readonly GameField _gameField;
        private readonly Hand _hand;
        private readonly BuildingsManager _buildingsManager;

        public BuildingPlacer(GameField gameField, Hand hand, BuildingsManager buildingsManager)
        {
            _gameField = gameField;
            _hand = hand;
            _buildingsManager = buildingsManager;
            _gameField.OnFieldClicked.Subscribe(FieldClickedHandler);
        }

        private void FieldClickedHandler(Vector2Int position)
        {
            Card? selectedCard = _hand.SelectedCard;
            if (selectedCard == null)
            {
                return;
            }

            if (!_gameField.CanAddBuildingTo(position))
            {
                return;
            }

            _buildingsManager.AddNewBuildingTo(selectedCard.BuildingType, position);
            _hand.RemoveCard(selectedCard);
        }
    }
}