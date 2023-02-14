#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.Buildings.Effect
{
    [CreateAssetMenu(menuName = "Configs/Building Effects/GiveEnergyInRadius", fileName = "give_energy_in_radius",
        order = 0)]
    public sealed class GiveEnergyInRadiusEffect : GiveResourceInRadiusEffect
    {
        protected override void GiveResource(Field field, int value)
        {
            field.AddEnergy(value);
        }
    }
}