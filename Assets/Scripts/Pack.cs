#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;
using Zenject;

namespace Solar2048
{
    public sealed class Pack
    {
        private readonly List<BuildingType> _buildingCards;

        public IReadOnlyList<BuildingType> BuildingCards => _buildingCards;

        public Pack(List<BuildingType> buildingCards)
        {
            _buildingCards = buildingCards;
        }
        
        public class Factory : PlaceholderFactory<List<BuildingType>, Pack>
        {
        }
    }
}