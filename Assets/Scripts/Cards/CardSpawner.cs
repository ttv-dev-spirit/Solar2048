#nullable enable
using Solar2048.Buildings;

namespace Solar2048.Cards
{
    public sealed class CardSpawner
    {
        private readonly Card.Factory _cardFactory;
        private readonly Hand _hand;

        public CardSpawner(Card.Factory cardFactory, Hand hand)
        {
            _cardFactory = cardFactory;
            _hand = hand;
        }

        public void AddCardToHand(BuildingType buildingType)
        {
            Card card = _cardFactory.Create(buildingType);
            _hand.AddCard(card);
        }

        public Card CreateCard(BuildingType buildingType) => _cardFactory.Create(buildingType);
    }
}