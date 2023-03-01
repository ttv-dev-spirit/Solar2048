#nullable enable
using Solar2048.AssetManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Buildings.UI
{
    public sealed class BuildingImage : MonoBehaviour
    {
        private BuildingType _buildingType;

        [SerializeField]
        private Image _image = null!;

        public BuildingType BuildingType => _buildingType;

        [Inject]
        private void Construct(BuildingType buildingType, IBuildingSettingsProvider buildingSettingsProvider,
            IAssetProvider assetProvider)
        {
            _buildingType = buildingType;
            BuildingSettings buildingSettings = buildingSettingsProvider.GetBuildingSettingsFor(_buildingType);
            SetImage(buildingSettings, assetProvider);
        }

        private async void SetImage(BuildingSettings buildingSettings, IAssetProvider assetProvider)
        {
            var image = await assetProvider.Load<Sprite>(buildingSettings.Image);
            _image.sprite = image;
        }


        public class Factory : PlaceholderFactory<BuildingType, BuildingImage>
        {
        }
    }
}