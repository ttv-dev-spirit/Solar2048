#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;

namespace Solar2048
{
    public interface IBuildingsPackProvider
    {
        List<BuildingType> GetBuildings(int count);
    }
}