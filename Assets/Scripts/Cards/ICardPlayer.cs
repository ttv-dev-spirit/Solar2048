#nullable enable
using Solar2048.Map;

namespace Solar2048.Cards
{
    public interface ICardPlayer
    {
        public void PlayCardFromHandTo(Card card, Tile tile);
    }
}