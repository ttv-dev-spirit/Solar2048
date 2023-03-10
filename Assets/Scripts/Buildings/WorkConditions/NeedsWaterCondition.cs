#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.Buildings.WorkConditions
{
    [CreateAssetMenu(menuName = "Configs/Building Work Condition/NeedsWaterCondition",
        fileName = "needs_water_condition",
        order = 0)]
    public sealed class NeedsWaterCondition : BuildingWorkCondition
    {
        [SerializeField]
        private int _value = 1;

        public override bool IsConditionMet(GameMap gameMap, Building building)
        {
            Tile tile = gameMap.GetTile(building.Position);
            return tile.Water.Value >= _value * Mathf.Pow(2, building.Level.Value - 1);
        }
    }
}