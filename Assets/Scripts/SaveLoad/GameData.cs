#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;
using Solar2048.Map;

namespace Solar2048.SaveLoad
{
    public sealed class GameData
    {
        public int Score;
        public List<BuildingType> Hand = new();
        public List<BuildingData> Buildings = new();
        public MoveDirection NextDirection;
        public int Cycle;
        public int PacksBought;
    }
}