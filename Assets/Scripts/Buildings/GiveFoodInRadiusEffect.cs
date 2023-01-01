﻿#nullable enable
using UnityEngine;

namespace Solar2048.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Building Effects/GiveFoodInRadius", fileName = "give_food_in_radius",
        order = 0)]
    public sealed class GiveFoodInRadiusEffect : GiveResourceInRadiusEffect
    {
        protected override void GiveResource(Field field, int value)
        {
            field.AddFood(value);
        }
    }
}