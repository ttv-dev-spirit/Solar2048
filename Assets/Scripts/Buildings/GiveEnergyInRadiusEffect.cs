#nullable enable
using UnityEngine;

namespace Solar2048.Buildings
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