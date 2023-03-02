#nullable enable
using UnityEngine;

namespace Solar2048.Packs
{
    [CreateAssetMenu(menuName = "Configs/Create Pack Buying Settings", fileName = "pack_buying_settings")]
    public sealed class PackBuyingSettings : ScriptableObject
    {
        [SerializeField]
        private int _fistPacksCost = 10;

        [SerializeField]
        private int _fixedCostPacksCount = 3;

        [SerializeField]
        private int _a = 10;

        [SerializeField]
        private int _b = -10;

        public int GetPackCost(int packNumber)
        {
            if (packNumber < _fixedCostPacksCount)
            {
                return _fistPacksCost;
            }

            return _a * packNumber + _b;
        }
    }
}