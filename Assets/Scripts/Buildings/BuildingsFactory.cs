#nullable enable

using JetBrains.Annotations;
using Solar2048.AssetManagement;
using Solar2048.Buildings.UI;
using UnityEngine;

namespace Solar2048.Buildings
{
    [UsedImplicitly]
    public sealed class BuildingsFactory : IBuildingsFactory
    {
        private readonly IBuildingSettingsProvider _settingsProvider;
        private readonly BuildingBehaviour.Factory _buildingBehaviourFactory;
        private readonly IAssetProvider _assetProvider;

        public BuildingsFactory(IBuildingSettingsProvider buildingSettingsProvider,
            BuildingBehaviour.Factory buildingBehaviourFactory, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _settingsProvider = buildingSettingsProvider;
            _buildingBehaviourFactory = buildingBehaviourFactory;
        }

        public Building Create(BuildingType buildingType)
        {
            BuildingSettings buildingSettings = _settingsProvider.GetBuildingSettingsFor(buildingType);
            BuildingBehaviour buildingBehaviour = _buildingBehaviourFactory.Create();
            var building = new Building(buildingSettings);
            buildingBehaviour.BindBuilding(building);
            SetBuildingImage(buildingBehaviour, buildingSettings);
            return building;
        }

        private async void SetBuildingImage(BuildingBehaviour building, BuildingSettings buildingSettings)
        {
            var image = await _assetProvider.Load<Sprite>(buildingSettings.Image);
            building.SetImage(image);
        }
    }
}