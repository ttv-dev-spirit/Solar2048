#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.Buildings.WorkConditions
{
    [CreateAssetMenu(menuName = "Configs/Building Work Condition/NeedsFoodCondition", fileName = "needs_food_condition",
        order = 0)]
    public sealed class NeedsFoodCondition : BuildingWorkCondition
    {
        [SerializeField]
        private int _value = 1;

        public override bool IsConditionMet(GameMap gameMap, Building building)
        {
            Tile tile = gameMap.GetTile(building.Position);
            return tile.Food.Value >= _value * Mathf.Pow(2, building.Level.Value - 1);
        }
    }
}