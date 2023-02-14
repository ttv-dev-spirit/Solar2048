#nullable enable
using Solar2048.Cards;
using UniRx;
using UnityEngine;

namespace Solar2048.Map
{
    public sealed class BuildingPlacer
    {
        private readonly GameMap _gameMap;
        private readonly Hand _hand;
        private readonly CardPlayer _cardPlayer;

        public BuildingPlacer(GameMap gameMap, Hand hand, CardPlayer cardPlayer)
        {
            _gameMap = gameMap;
            _hand = hand;
            _cardPlayer = cardPlayer;
            _gameMap.OnFieldClicked.Subscribe(FieldClickedHandler);
        }

        private void FieldClickedHandler(Vector2Int position)
        {
            Card? selectedCard = _hand.SelectedCard.Value;
            if (selectedCard == null)
            {
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

            if (!_cardPlayer.IsActive)
            {
                return;
            }

            _cardPlayer.PlayCardFromHandTo(selectedCard, field);
        }
    }
}