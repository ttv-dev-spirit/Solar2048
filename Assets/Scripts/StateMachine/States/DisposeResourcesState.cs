#nullable enable
using Solar2048.AssetManagement;
using Zenject;

namespace Solar2048.StateMachine.States
{
    public class DisposeResourcesState : State
    {
        private readonly IAssetProvider _assetProvider;

        public DisposeResourcesState(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        protected override void OnEnter()
        {
            _assetProvider.CleanUp();
            Finish();
        }

        protected override void OnExit()
        {
        }

        public class Factory : PlaceholderFactory<DisposeResourcesState>
        {
        }
    }
}