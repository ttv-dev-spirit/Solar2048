#nullable enable
using UnityEngine;

namespace Solar2048.Buildings
{
    public abstract class BuildingWorkCondition : ScriptableObject
    {
        public abstract bool IsConditionMet(GameMap gameMap, Building building);
    }
}