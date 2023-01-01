#nullable enable
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048.Buildings
{
    public sealed class BuildingBehaviour : MonoBehaviour
    {
        private const string LEVEL_TEXT = "level ";
        private GameFieldBehaviour _gameFieldBehaviour = null!;

        [SerializeField]
        private SpriteRenderer _image = null!;

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

        public void SubToLevel(IReadOnlyReactiveProperty<int> level)
        {
            level.Subscribe(OnLevelChanged);
        }

        private void OnLevelChanged(int level) => _level.text = LEVEL_TEXT + level;

        public class Factory : PlaceholderFactory<BuildingBehaviour>
        {
        }
    }
}