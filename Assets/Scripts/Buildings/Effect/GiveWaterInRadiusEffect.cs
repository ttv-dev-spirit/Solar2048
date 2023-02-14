#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.Buildings.Effect
{
    [CreateAssetMenu(menuName = "Configs/Building Effects/GiveWaterInRadius", fileName = "give_water_in_radius",
        order = 0)]
    public sealed class GiveWaterInRadiusEffect : GiveResourceInRadiusEffect
    {
        protected override void GiveResource(Field field, int value)
        {
            field.AddWater(value);
        }
    }
}