#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;
using UniRx;

namespace Solar2048
{
    public sealed class BuildingEffectsTrigger
    {
        private readonly BuildingsManager _buildingsManager;
        private readonly GameMap _gameMap;

        public BuildingEffectsTrigger(BuildingsManager buildingsManager, GameMap gameMap)
        {
            _buildingsManager = buildingsManager;
            _gameMap = gameMap;
            _gameMap.OnTriggerBuildingsEffects.Subscribe(TriggerBuildingEffects);
        }

        private void TriggerBuildingEffects(Unit _)
        {
            var buildings = _buildingsManager.Buildings;
            var triggeredBuilding = new List<Building>();
            var anythingTriggered = true;
            while (buildings.Count != triggeredBuilding.Count && anythingTriggered)
            {
                anythingTriggered = false;
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
            }
        }

        private bool TryTriggerEffects(Building building)
        {
            if (!building.AreConditionsMet())
            {
                return false;
            }

            building.ExecuteEffects();
            return true;
        }
    }
}