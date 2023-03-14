#nullable enable

using System.Collections.Generic;
using System.Linq;
using Solar2048.AssetManagement;
using Solar2048.Infrastructure;
using Solar2048.Map;
using Solar2048.SaveLoad;

namespace Solar2048.Buildings
{
    public sealed class BuildingsManager : IResetable, ISavable, ILoadable, IBuildingsManager
    {
        private readonly IBuildingsFactory _buildingsFactory;
        private readonly GameMap _gameMap;

        private readonly List<Building> _buildings = new();

        public IReadOnlyList<Building> Buildings => _buildings;

        public BuildingsManager(IBuildingsFactory buildingsFactory, GameMap gameMap, ISaveRegister saveRegister)
        {
            _gameMap = gameMap;
            _buildingsFactory = buildingsFactory;
            saveRegister.Register(this);
        }

        public void AddNewBuilding(BuildingType buildingType, Tile tile)
        {
            Building building = _buildingsFactory.Create(buildingType);
            _buildings.Add(building);
            tile.AddBuilding(building);
        }

        public void RemoveBuilding(Building building)
        {
            Tile tile = _gameMap.GetTile(building.Position);
            tile.RemoveBuilding();
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

        private void AddBuilding(BuildingData buildingData)
        {
            Tile tile = _gameMap.GetTile(buildingData.Position);
            AddNewBuilding(buildingData.BuildingType, tile);
            Building building = tile.Building!;
            building.SetLevel(buildingData.Level);
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