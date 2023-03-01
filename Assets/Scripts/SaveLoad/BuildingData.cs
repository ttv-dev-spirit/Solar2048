#nullable enable
using System;
using Solar2048.Buildings;
using UnityEngine;

namespace Solar2048.SaveLoad
{
    [Serializable]
    public class BuildingData
    {
        public BuildingType BuildingType;
        public Vector2Int Position;
        public int Level;
    }
}