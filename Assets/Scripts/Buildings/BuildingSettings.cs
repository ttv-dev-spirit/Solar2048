#nullable enable
using System;
using Solar2048.Buildings.Effect;
using Solar2048.Buildings.WorkConditions;
using UnityEngine;

namespace Solar2048.Buildings
{
    [Serializable]
    public sealed class BuildingSettings
    {
        public BuildingType BuildingType;
        public Sprite Image = null!;
        public string Name = null!;
        public BuildingWorkCondition[]? WorkConditions;
        public BuildingEffect[]? BuildingEffects;
    }
}