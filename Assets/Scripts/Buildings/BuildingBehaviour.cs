#nullable enable
using UnityEngine;
using Zenject;

namespace Solar2048.Buildings
{
    public sealed class BuildingBehaviour : MonoBehaviour
    {
        private GameFieldBehaviour _gameFieldBehaviour = null!;

        [SerializeField]
        private SpriteRenderer _image = null!;

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

        public class Factory : PlaceholderFactory<BuildingBehaviour>
        {
        }
    }
}