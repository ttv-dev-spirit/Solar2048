#nullable enable
using UnityEngine;

namespace Solar2048.UI.Skins
{
    public class SkinController : MonoBehaviour
    {
        [SerializeField]
        private SkinReactor[] _reactors = null!;

        public void ActivateSkin(int skinID)
        {
            foreach (SkinReactor skinReactor in _reactors)
            {
                skinReactor.ActivateSkin(skinID);
            }
        }
    }
}