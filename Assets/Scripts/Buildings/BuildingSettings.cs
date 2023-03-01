#nullable enable
using System.Collections.Generic;
using System.Linq;
using Solar2048.Buildings.Effect;
using Solar2048.Buildings.WorkConditions;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Solar2048.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Building Settings", fileName = "BuildingSettings", order = 0)]
    public sealed class BuildingSettings : ScriptableObject
    {
        [SerializeField]
        private BuildingType _buildingType;

        [SerializeField]
        private AssetReferenceAtlasedSprite _image = null!;

        [SerializeField]
        private BuildingWorkCondition[]? _workConditions;

        [SerializeField]
        private BuildingEffect[]? _buildingEffects;

        public BuildingType BuildingType => _buildingType;
        public AssetReferenceAtlasedSprite Image => _image;

        public IEnumerable<BuildingWorkCondition> WorkConditions =>
            _workConditions ?? Enumerable.Empty<BuildingWorkCondition>();

        public IEnumerable<BuildingEffect> BuildingEffects =>
            _buildingEffects ?? Enumerable.Empty<BuildingEffect>();
    }
}