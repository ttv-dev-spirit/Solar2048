#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;

namespace Solar2048.Packs
{
    public interface IBuildingsPackProvider
    {
        List<BuildingType> GetBuildings(int count);
    }
}