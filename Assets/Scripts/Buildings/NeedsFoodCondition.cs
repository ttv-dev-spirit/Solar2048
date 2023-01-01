#nullable enable
using UnityEngine;

namespace Solar2048.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Building Work Condition/NeedsFoodCondition", fileName = "needs_food_condition",
        order = 0)]
    public sealed class NeedsFoodCondition : BuildingWorkCondition
    {
        [SerializeField]
        private int _value = 1;

        public override bool IsConditionMet(GameMap gameMap, Building building)
        {
            Field field = gameMap.GetField(building.Position);
            return field.Food.Value >= _value;
        }
    }
}