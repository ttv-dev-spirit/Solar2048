#nullable enable
using UnityEngine;

namespace Solar2048
{
    [CreateAssetMenu(menuName = "Configs/Create Pack Buying Settings", fileName = "pack_buying_settings")]
    public sealed class PackBuyingSettings : ScriptableObject
    {
        [SerializeField]
        private int _packCost = 1;

        public int PackCost => _packCost;
    }
}