#nullable enable
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
        private void Construct(BuildingType buildingType, IBuildingSettingsContainer buildingFactorySettings)
        {
            _buildingType = buildingType;
            BuildingSettings buildingSettings = buildingFactorySettings.GetBuildingSettingsFor(_buildingType);
            _image.sprite = buildingSettings.Image;
        }

        public class Factory : PlaceholderFactory<BuildingType, BuildingImage>
        {
        }
    }
}