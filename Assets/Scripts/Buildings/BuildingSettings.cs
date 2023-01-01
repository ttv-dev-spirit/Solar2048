#nullable enable
using System;
using UnityEngine;

namespace Solar2048.Buildings
{
    [Serializable]
    public sealed class BuildingSettings
    {
        public BuildingType BuildingType;
        public Sprite Image = null!;
        public string Name = null!;
    }
}