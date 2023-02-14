#nullable enable
using Solar2048.Map;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048.Buildings.UI
{
    public sealed class BuildingBehaviour : MonoBehaviour
    {
        private GameFieldBehaviour _gameFieldBehaviour = null!;

        [SerializeField]
        private SpriteRenderer _image = null!;

        [SerializeField]
        private GameObject _conditionsMetBorder = null!;

        [SerializeField]
        private TMP_Text _level = null!;

        [Inject]
        private void Construct(GameFieldBehaviour gameFieldBehaviour)
        {
            _gameFieldBehaviour = gameFieldBehaviour;
        }

        public void SetImage(Sprite image) => _image.sprite = image;

        public void SetPosition(Vector2Int position)
        {
            transform.position = _gameFieldBehaviour.PositionToWorld(position);
        }

        public void Sub(IReadOnlyReactiveProperty<int> level, IReadOnlyReactiveProperty<bool> areConditionsMet)
        {
            level.Subscribe(OnLevelChanged);
            areConditionsMet.Subscribe(OnConditionsMetChanged);
        }

        private void OnLevelChanged(int level) => _level.text = level.ToString();
        private void OnConditionsMetChanged(bool areConditionsMet) => _conditionsMetBorder.SetActive(areConditionsMet);

        public class Factory : PlaceholderFactory<BuildingBehaviour>
        {
        }
    }
}