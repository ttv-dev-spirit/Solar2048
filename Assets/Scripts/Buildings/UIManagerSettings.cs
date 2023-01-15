#nullable enable
using UnityEngine;

namespace Solar2048.Buildings
{
    [CreateAssetMenu(menuName = "Create UIManagerSettings", fileName = "UIManagerSettings", order = 0)]
    public sealed class UIManagerSettings : ScriptableObject
    {
        [SerializeField]
        private LayerMask _layerMask;

        public LayerMask LayerMask => _layerMask;
    }
}