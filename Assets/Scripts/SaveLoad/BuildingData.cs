#nullable enable
using Solar2048.Buildings;
using Unity.Properties;
using UnityEngine;

namespace Solar2048.SaveLoad
{
    [GeneratePropertyBag]
    public class BuildingData
    {
        public BuildingType BuildingType;
        public Vector2Int Position;
        public int Level;
    }
}