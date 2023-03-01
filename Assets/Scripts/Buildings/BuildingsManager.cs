#nullable enable

using System.Collections.Generic;
using System.Linq;
using Solar2048.AssetManagement;
using Solar2048.Infrastructure;
using Solar2048.Map;
using Solar2048.SaveLoad;

namespace Solar2048.Buildings
{
    public sealed class BuildingsManager : IResetable, ISavable, ILoadable
    {
        private readonly BuildingsFactory _buildingsFactory;
        private readonly GameMap _gameMap;

        private readonly List<Building> _buildings = new();

        public IReadOnlyList<Building> Buildings => _buildings;

        public BuildingsManager(BuildingsFactory buildingsFactory, GameMap gameMap, SaveController saveController)
        {
            _gameMap = gameMap;
            _buildingsFactory = buildingsFactory;
            saveController.Register(this);
        }

        public void AddNewBuilding(BuildingType buildingType, Field field)
        {
            Building building = _buildingsFactory.Create(buildingType);
            _buildings.Add(building);
            field.AddBuilding(building);
        }

        public void AddBuilding(BuildingData buildingData)
        {
            Field field = _gameMap.GetField(buildingData.Position);
            AddNewBuilding(buildingData.BuildingType, field);
            Building building = field.Building!;
            building.SetLevel(buildingData.Level);
        }

        public void RemoveBuilding(Building building)
        {
            _buildings.Remove(building);
            building.Destroy();
        }

        public void Reset()
        {
            Building[] buildingsToDestroy = _buildings.ToArray();
            for (var i = 0; i < buildingsToDestroy.Length; i++)
            {
                RemoveBuilding(buildingsToDestroy[i]);
            }
        }

        public void Save(GameData gameData)
        {
            gameData.Buildings = _buildings.Select(ToBuildingData).ToList();
        }

        public void Load(GameData gameData)
        {
            foreach (BuildingData building in gameData.Buildings)
            {
                AddBuilding(building);
            }
        }

        private static BuildingData ToBuildingData(Building building)
        {
            return new BuildingData()
            {
                BuildingType = building.BuildingType,
                Level = building.Level.Value,
                Position = building.Position
            };
        }
    }
}