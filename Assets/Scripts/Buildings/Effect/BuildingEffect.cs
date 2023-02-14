#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.Buildings.Effect
{
    public abstract class BuildingEffect : ScriptableObject
    {
        public abstract void ExecuteEffect(GameMap gameMap, Building building);
    }
}