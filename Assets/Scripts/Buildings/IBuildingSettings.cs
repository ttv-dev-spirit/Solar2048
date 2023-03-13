#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings.Effect;
using Solar2048.Buildings.WorkConditions;
using UnityEngine.AddressableAssets;

namespace Solar2048.Buildings
{
    public interface IBuildingSettings
    {
        BuildingType BuildingType { get; }
        AssetReferenceAtlasedSprite Image { get; }
        IEnumerable<BuildingWorkCondition> WorkConditions { get; }
        IEnumerable<BuildingEffect> BuildingEffects { get; }
    }
}