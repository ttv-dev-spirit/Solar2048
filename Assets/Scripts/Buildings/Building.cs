#nullable enable
using System;
using Solar2048.Buildings.Effect;
using Solar2048.Buildings.WorkConditions;
using Solar2048.Map;
using UniRx;
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class Building
    {
        private readonly Subject<Unit> _onPositionChanged = new();
        private readonly Subject<Building> _onDestroy = new();
        private readonly BuildingSettings _buildingSettings;
        private readonly GameMap _gameMap;

        private readonly ReactiveProperty<int> _level = new(1);
        private readonly ReactiveProperty<bool> _areConditionsMet = new();

        public BuildingType BuildingType => _buildingSettings.BuildingType;
        public Vector2Int Position { get; private set; }

        public IReadOnlyReactiveProperty<int> Level => _level;
        public IReadOnlyReactiveProperty<bool> AreConditionsMet => _areConditionsMet;
        public IObservable<Unit> OnPositionChanged => _onPositionChanged;
        public IObservable<Building> OnDestroy => _onDestroy;

        public Building(BuildingSettings buildingSettings, GameMap gameMap)
        {
            _buildingSettings = buildingSettings;
            _gameMap = gameMap;
        }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
            _onPositionChanged.OnNext(Unit.Default);
        }

        public void UpLevel() => _level.Value++;

        public bool CanBeMerged(Building building)
        {
            return BuildingType == building.BuildingType && Level.Value == building.Level.Value;
        }

        public void Destroy()
        {
            _onDestroy.OnNext(this);
        }

        public bool CheckConditionsMet()
        {
            var conditions = _buildingSettings.WorkConditions;
            if (conditions == null)
            {
                _areConditionsMet.Value = true;
                return true;
            }

            foreach (BuildingWorkCondition buildingWorkCondition in conditions)
            {
                if (!buildingWorkCondition.IsConditionMet(_gameMap, this))
                {
                    _areConditionsMet.Value = false;
                    return false;
                }
            }

            _areConditionsMet.Value = true;
            return true;
        }

        public void ExecuteEffects()
        {
            var effects = _buildingSettings.BuildingEffects;
            if (effects == null)
            {
                return;
            }

            foreach (BuildingEffect buildingEffect in effects)
            {
                buildingEffect.ExecuteEffect(_gameMap, this);
            }
        }
    }
}