#nullable enable
using Solar2048.Buildings;
using Solar2048.Cards;
using Zenject;

namespace Solar2048.Cheats
{
    public sealed class CheatCardsSupplier : ICheat
    {
        private readonly CardSpawner _cardSpawner;

        public bool IsActive { get; private set; }

        public CheatCardsSupplier(CardSpawner cardSpawner)
        {
            _cardSpawner = cardSpawner;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Reset()
        {
            _cardSpawner.AddCardToHand(BuildingType.SolarPanel);
            _cardSpawner.AddCardToHand(BuildingType.WindTurbine);
            _cardSpawner.AddCardToHand(BuildingType.Greenhouse);
            _cardSpawner.AddCardToHand(BuildingType.WaterCollector);
            _cardSpawner.AddCardToHand(BuildingType.WaterCollector);
        }

        public class Factory : PlaceholderFactory<CheatCardsSupplier>
        {
        }
    }
}