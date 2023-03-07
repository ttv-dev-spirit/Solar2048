#nullable enable
using UnityEngine;

namespace Solar2048.Packs
{
    [CreateAssetMenu(menuName = "Configs/Create Pack Buying Settings", fileName = "pack_buying_settings")]
    public sealed class PackBuyingSettings : ScriptableObject
    {
        [SerializeField]
        private int _a = 10;

        [SerializeField]
        private int _b = -10;

        public int GetPackCost(int packNumber)
        {
            return _a * (packNumber + 1) + _b;
        }
    }
}