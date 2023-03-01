#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;
using Solar2048.Map;
using Unity.Properties;

namespace Solar2048.SaveLoad
{
    public sealed class GameData
    {
        public int CurrentScore;
        public int TotalScore;
        public List<BuildingType> Hand = new();
        public List<BuildingData> Buildings = new();
        public MoveDirection NextDirection;
    }
}