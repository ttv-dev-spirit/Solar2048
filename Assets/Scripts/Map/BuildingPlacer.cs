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
        private readonly ICardPlayer _cardPlayer;

        public BuildingPlacer(GameMap gameMap, Hand hand, ICardPlayer cardPlayer)
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

            Tile tile = _gameMap.GetTile(position);
            if (tile.Building != null)
            {
                return;
            }

            _cardPlayer.PlayCardFromHandTo(selectedCard, tile);
        }
    }
}