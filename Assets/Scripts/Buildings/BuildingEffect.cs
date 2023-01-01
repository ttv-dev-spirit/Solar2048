#nullable enable
using System;
using UnityEngine;

namespace Solar2048.Buildings
{
    public abstract class BuildingEffect : ScriptableObject
    {
        public abstract void ExecuteEffect(GameMap gameMap, Building building);
    }
}