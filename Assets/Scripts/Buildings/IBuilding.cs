#nullable enable
namespace Solar2048.Buildings
{
    public abstract class Building
    {
        private readonly BuildingBehaviour _behaviour;
        private readonly BuildingSettings _buildingSettings;

        protected Building(BuildingSettings buildingSettings, BuildingBehaviour behaviour)
        {
            _buildingSettings = buildingSettings;
            _behaviour = behaviour;
        }

        public void SetPosition(int x, int y)
        {
        }
    }
}