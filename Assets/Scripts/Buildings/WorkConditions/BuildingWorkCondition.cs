#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.Buildings.WorkConditions
{
    public abstract class BuildingWorkCondition : ScriptableObject
    {
        public abstract bool IsConditionMet(GameMap gameMap, Building building);
    }
}