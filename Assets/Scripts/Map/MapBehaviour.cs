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
            float x = mapPosition.x * _tileSize.x;
            float z = mapPosition.y * _tileSize.y;
            var position = new Vector3(x, 0, z);
            return position + transform.position;
        }

        public Vector2Int TileWorldToMap(Vector3 position)
        {
            float x = position.x / _tileSize.x;
            float y = position.z / _tileSize.y;
            return new Vector2Int((int)x, (int)y);
        }
    }
}