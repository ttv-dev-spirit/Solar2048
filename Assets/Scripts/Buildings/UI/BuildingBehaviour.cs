#nullable enable
using Solar2048.Map;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Solar2048.Buildings.UI
{
    public sealed class BuildingBehaviour : MonoBehaviour
    {
        private Building _building = null!;
        private MapBehaviour _mapBehaviour = null!;

        [SerializeField]
        private QuadImage _quadImage = null!;

        [SerializeField]
        private GameObject _conditionsMetBorder = null!;

        [SerializeField]
        private TMP_Text _level = null!;

        [Inject]
        private void Construct(MapBehaviour mapBehaviour)
        {
            _mapBehaviour = mapBehaviour;
        }

        public void BindBuilding(Building building)
        {
            _building = building;
            _building.OnPositionChanged.Subscribe(UpdatePosition);
            _building.Level.Subscribe(OnLevelChanged);
            _building.AreConditionsMet.Subscribe(OnConditionsMetChanged);
            _building.OnDestroy.Subscribe(DestroyHandler);
        }

        private void UpdatePosition(Unit _)
            => transform.position = _mapBehaviour.BuildingMapToWorld(_building.Position);

        private void OnLevelChanged(int level) => _level.text = level.ToString();
        private void OnConditionsMetChanged(bool areConditionsMet) => _conditionsMetBorder.SetActive(areConditionsMet);

        private void DestroyHandler(Building building)
        {
            Assert.IsTrue(_building == building);
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<BuildingBehaviour>
        {
        }

        public void SetImage(Sprite image) => _quadImage.SetImage(image);
    }
}