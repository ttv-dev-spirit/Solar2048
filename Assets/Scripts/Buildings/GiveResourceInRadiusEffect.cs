#nullable enable
using UnityEngine;

namespace Solar2048.Buildings
{
    public abstract class GiveResourceInRadiusEffect : BuildingEffect
    {
        [SerializeField]
        protected int _radius = 1;

        [SerializeField]
        protected int _value = 1;
        
        public override void ExecuteEffect(GameMap gameMap, Building building)
        {
            Vector2Int position = building.Position;
            int yMin = Mathf.Max(0, position.y - _radius);
            int yMax = Mathf.Min(GameMap.FIELD_SIZE - 1, position.y + _radius);
            int xMin = Mathf.Max(0, position.x - _radius);
            int xMax = Mathf.Min(GameMap.FIELD_SIZE - 1, position.x + _radius);
            int resourceToGive = _value * (int)Mathf.Pow(2, building.Level.Value - 1);
            for (int y = yMin; y <= yMax; y++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    Field field = gameMap.GetField(x, y);
                    GiveResource(field, resourceToGive);
                }
            }
        }

        protected abstract void GiveResource(Field field, int value);
    }
}