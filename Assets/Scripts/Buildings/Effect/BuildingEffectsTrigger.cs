#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings.WorkConditions;
using Solar2048.Map;
using UniRx;

namespace Solar2048.Buildings.Effect
{
    public sealed class BuildingEffectsTrigger
    {
        private readonly IBuildingsManager _buildingsManager;
        private readonly GameMap _gameMap;

        public BuildingEffectsTrigger(IBuildingsManager buildingsManager, GameMap gameMap)
        {
            _buildingsManager = buildingsManager;
            _gameMap = gameMap;
            _gameMap.OnTriggerBuildingsEffects.Subscribe(TriggerBuildingEffects);
        }

        private void TriggerBuildingEffects(Unit _)
        {
            IReadOnlyList<Building>? buildings = _buildingsManager.Buildings;
            var triggeredBuilding = new List<Building>();
            while (buildings.Count != triggeredBuilding.Count
                   && TriggerBuildings(buildings, triggeredBuilding))
            {
            }

            foreach (Building building in buildings)
            {
                building.SetConditionsMet(triggeredBuilding.Contains(building));
            }
        }

        private bool TriggerBuildings(IReadOnlyList<Building> buildings, List<Building> triggeredBuilding)
        {
            var anythingTriggered = false;
            foreach (Building building in buildings)
            {
                if (triggeredBuilding.Contains(building))
                {
                    continue;
                }

                if (!TryTriggerEffects(building))
                {
                    continue;
                }

                anythingTriggered = true;
                triggeredBuilding.Add(building);
            }

            return anythingTriggered;
        }

        private bool CheckConditionsMet(Building building)
        {
            var conditions = building.WorkConditions;

            foreach (BuildingWorkCondition buildingWorkCondition in conditions)
            {
                if (!buildingWorkCondition.IsConditionMet(_gameMap, building))
                {
                    return false;
                }
            }

            return true;
        }

        private void ExecuteEffects(Building building)
        {
            var effects = building.BuildingEffects;

            foreach (BuildingEffect buildingEffect in effects)
            {
                buildingEffect.ExecuteEffect(_gameMap, building);
            }
        }

        private bool TryTriggerEffects(Building building)
        {
            if (!CheckConditionsMet(building))
            {
                return false;
            }

            ExecuteEffects(building);
            return true;
        }
    }
}