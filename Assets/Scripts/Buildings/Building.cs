#nullable enable
using UniRx;
using UnityEngine;

namespace Solar2048.Buildings
{
    public abstract class Building
    {
        private ReactiveProperty<int> _level = new(1);

        private readonly BuildingBehaviour _behaviour;
        private readonly BuildingSettings _buildingSettings;

        private Vector2Int _position;

        public IReadOnlyReactiveProperty<int> Level => _level;

        protected Building(BuildingSettings buildingSettings, BuildingBehaviour behaviour)
        {
            _buildingSettings = buildingSettings;
            _behaviour = behaviour;
            behaviour.SubToLevel(Level);
        }

        public void SetPosition(Vector2Int position)
        {
            _position = position;
            _behaviour.SetPosition(position);
        }

        public void UpLevel() => _level.Value++;

        public bool CanBeMerged(Building building)
        {
            return GetType() == building.GetType() && Level.Value == building.Level.Value;
        }

        public void Destroy()
        {
            Object.Destroy(_behaviour.gameObject);
        }
    }
}