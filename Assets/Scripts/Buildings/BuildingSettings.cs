#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Solar2048.Buildings.Effect;
using Solar2048.Buildings.WorkConditions;
using UnityEngine;

namespace Solar2048.Buildings
{
    [Serializable]
    public sealed class BuildingSettings
    {
        [SerializeField]
        private BuildingType _buildingType;

        [SerializeField]
        private Sprite _image = null!;

        [SerializeField]
        private string _name = null!;

        [SerializeField]
        private BuildingWorkCondition[]? _workConditions;

        [SerializeField]
        private BuildingEffect[]? _buildingEffects;

        public BuildingType BuildingType => _buildingType;
        public string Name => _name;
        public Sprite Image => _image;

        public IEnumerable<BuildingWorkCondition> WorkConditions =>
            _workConditions ?? Enumerable.Empty<BuildingWorkCondition>();

        public IEnumerable<BuildingEffect> BuildingEffects =>
            _buildingEffects ?? Enumerable.Empty<BuildingEffect>();
    }
}