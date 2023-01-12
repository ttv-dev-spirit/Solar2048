#nullable enable
using UnityEngine;

namespace Solar2048
{
    [CreateAssetMenu(menuName = "Configs/Create PackGeneratorSettings", fileName = "pack_generator_settings",
        order = 0)]
    public sealed class PackGeneratorSettings : ScriptableObject
    {
        [SerializeField]
        private int _cardsInPack = 1;

        public int CardsInPack => _cardsInPack;
    }
}