#nullable enable
using UnityEngine;

namespace Solar2048.Buildings
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
            Field field = gameMap.GetField(building.Position);
            return field.Water.Value >= _value * Mathf.Pow(2, building.Level.Value - 1);
        }
    }
}