#nullable enable
using UnityEngine;

namespace Solar2048.Map
{
    public sealed class MapBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _tileSize;

        public Vector3 BuildingMapToWorld(Vector2Int mapPosition)
        {
            var position = new Vector3(mapPosition.x * _tileSize.x, 0, mapPosition.y * _tileSize.y);
            return position += transform.position;
        }

        public Vector2Int TileWorldToMap(Vector3 position)
        {
            float x = position.x / _tileSize.x;
            float y = position.z / _tileSize.y;
            return new Vector2Int((int)x, (int)y);
        }
    }
}