#nullable enable
using System.Collections.Generic;

namespace Solar2048.Cards
{
    public interface ICardContainer
    {
        public void AddCard(Card card);
        public void RemoveCard(Card card);
        public IReadOnlyList<Card> Cards { get; }
    }
}