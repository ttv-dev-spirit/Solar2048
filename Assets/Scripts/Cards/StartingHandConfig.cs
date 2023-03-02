#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;
using UnityEngine;

namespace Solar2048.Cards
{
    [CreateAssetMenu(menuName = "Configs/starting_hand_config", fileName = "starting_hand_config", order = 0)]
    public sealed class StartingHandConfig : ScriptableObject
    {
        [SerializeField]
        private BuildingType[] _startingHand = null!;

        public IEnumerable<BuildingType> StartingHand => _startingHand;
    }
}