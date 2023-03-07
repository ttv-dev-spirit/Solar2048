#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.Buildings.Effect
{
    [CreateAssetMenu(menuName = "Configs/Building Effects/GiveFoodInRadius", fileName = "give_food_in_radius",
        order = 0)]
    public sealed class GiveFoodInRadiusEffect : GiveResourceInRadiusEffect
    {
        protected override void GiveResource(Tile tile, int value)
        {
            tile.AddFood(value);
        }
    }
}