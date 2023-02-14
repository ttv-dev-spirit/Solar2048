#nullable enable
namespace Solar2048.UI.Skins
{
    public sealed class ActivatableSkinController : SkinController, IActivatable
    {
        private const int DEACTIVATED_SKIN_ID = 0;
        private const int ACTIVATED_SKIN_ID = 1;

        public bool IsActive { get; private set; }

        public void Activate()
        {
            IsActive = true;
            ActivateSkin(ACTIVATED_SKIN_ID);
        }

        public void Deactivate()
        {
            IsActive = true;
            ActivateSkin(DEACTIVATED_SKIN_ID);
        }
    }
}