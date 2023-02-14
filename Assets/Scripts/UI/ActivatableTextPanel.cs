#nullable enable
using Solar2048.UI.Skins;
using TMPro;
using UnityEngine;

namespace Solar2048.UI
{
    public sealed class ActivatableTextPanel : MonoBehaviour, IActivatable
    {
        [SerializeField]
        private ActivatableSkinController _skinController = null!;

        [SerializeField]
        private TMP_Text _text = null!;

        public void SetText(string text)
        {
            _text.text = text;
        }

        public bool IsActive => _skinController.IsActive;
        public void Activate() => _skinController.Activate();
        public void Deactivate() => _skinController.Deactivate();
    }
}