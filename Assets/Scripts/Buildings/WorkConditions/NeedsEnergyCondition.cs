#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.Buildings.WorkConditions
{
    [CreateAssetMenu(menuName = "Configs/Building Work Condition/NeedsEnergyCondition",
        fileName = "needs_energy_condition",
        order = 0)]
    public sealed class NeedsEnergyCondition : BuildingWorkCondition
    {
        [SerializeField]
        private int _value = 1;

        public override bool IsConditionMet(GameMap gameMap, Building building)
        {
            Field field = gameMap.GetField(building.Position);
            return field.Energy.Value >= _value * Mathf.Pow(2, building.Level.Value - 1);
        }
    }
}