#nullable enable
using Solar2048.Map;
using Solar2048.UI.Skins;
using UnityEngine;

namespace Solar2048.UI
{
    public sealed class NextMovePanel : MonoBehaviour, IActivatable
    {
        [SerializeField]
        private ActivatableSkinController _activatableSkinController = null!;

        [SerializeField]
        private RectTransform _arrow = null!;

        public bool IsActive => _activatableSkinController.IsActive;

        public void Activate() => _activatableSkinController.Activate();
        public void Deactivate() => _activatableSkinController.Deactivate();

        public void SetDirection(MoveDirection direction)
        {
            _arrow.rotation = Quaternion.Euler(0, 0, -90 * (int)direction);
        }
    }
}