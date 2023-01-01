#nullable enable
using System.Linq;
using UnityEngine;

namespace Solar2048.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Building Factory Settings", fileName = "building_factory_settings", order = 0)]
    public sealed class BuildingFactorySettings : ScriptableObject
    {
        [SerializeField]
        private BuildingSettings[] _buildingSettings = null!;

        public BuildingSettings GetBuildingSettingsFor(BuildingType buildingType) =>
            _buildingSettings.First(setting => setting.BuildingType == buildingType);
    }
}