#nullable enable
using UnityEngine;

namespace Solar2048
{
    public sealed class GameFieldBehaviour : MonoBehaviour
    {
        public Vector3 PositionToWorld(Vector2Int position) => new Vector3(position.x, -position.y);

        public Vector2Int GetFieldPosition(FieldBehaviour fieldBehaviour)
        {
            Vector3 position = fieldBehaviour.transform.position;
            return new Vector2Int((int)position.x, -(int)position.y);
        }

        public bool TryWorldToPosition(Vector3 wordPosition, out Vector2Int position)
        {
            var x = (int)wordPosition.x;
            int y = -(int)wordPosition.y;
            if (!IsInsideBounds(GameMap.FIELD_SIZE))
            {
                position = Vector2Int.zero;
                return false;
            }

            position = new Vector2Int(x, y);
            return true;

            bool IsInsideBounds(int size) => x >= 0 && x < size && y >= 0 && y < size;
        }
    }
}