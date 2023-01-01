#nullable enable
using UnityEngine;
using Zenject;

namespace Solar2048.Buildings
{
    public abstract class Building
    {
        private readonly BuildingBehaviour _behaviour;
        private readonly BuildingSettings _buildingSettings;

        private Vector2Int _position;

        protected Building(BuildingSettings buildingSettings, BuildingBehaviour behaviour)
        {
            _buildingSettings = buildingSettings;
            _behaviour = behaviour;
        }

        public void SetPosition(Vector2Int position)
        {
            _position = position;
            _behaviour.SetPosition(position);
        }

    }
}