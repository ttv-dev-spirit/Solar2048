#nullable enable

using System;
using System.Collections.Generic;
using Solar2048.Buildings.Effect;
using Solar2048.Buildings.WorkConditions;
using UniRx;
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class Building
    {
        private readonly BuildingSettings _buildingSettings;
        
        private readonly Subject<Unit> _onPositionChanged = new();
        private readonly Subject<Building> _onDestroy = new();
        private readonly ReactiveProperty<bool> _areConditionsMet = new();
        private readonly ReactiveProperty<int> _level = new(1);

        public BuildingType BuildingType => _buildingSettings.BuildingType;
        public Vector2Int Position { get; private set; }

        public IReadOnlyReactiveProperty<int> Level => _level;
        public IObservable<Unit> OnPositionChanged => _onPositionChanged;
        public IObservable<Building> OnDestroy => _onDestroy;
        public IEnumerable<BuildingWorkCondition> WorkConditions => _buildingSettings.WorkConditions;
        public IEnumerable<BuildingEffect> BuildingEffects => _buildingSettings.BuildingEffects;
        public IReadOnlyReactiveProperty<bool> AreConditionsMet => _areConditionsMet;

        public Building(BuildingSettings buildingSettings)
        {
            _buildingSettings = buildingSettings;
        }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
            _onPositionChanged.OnNext(Unit.Default);
        }

        public void SetConditionsMet(bool areConditionsMet) => _areConditionsMet.Value = areConditionsMet;

        public void UpLevel() => _level.Value++;

        public void SetLevel(int level) => _level.Value = level;

        public bool CanBeMerged(Building building)
        {
            return BuildingType == building.BuildingType && Level.Value == building.Level.Value;
        }

        public void Destroy()
        {
            _onDestroy.OnNext(this);
        }
    }
}