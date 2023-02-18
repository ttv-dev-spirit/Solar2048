﻿#nullable enable
using System.Collections.Generic;
using Solar2048.Map;
using UniRx;

namespace Solar2048.Buildings.Effect
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
            if (!building.CheckConditionsMet())
            {
                return false;
            }

            building.ExecuteEffects();
            return true;
        }
    }
}