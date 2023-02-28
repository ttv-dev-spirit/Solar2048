#nullable enable
using Solar2048.AssetManagement;
using Solar2048.Cheats;
using Zenject;

namespace Solar2048.StateMachine.Game.States
{
    public class DisposeResourcesState : State
    {
        private readonly IAssetProvider _assetProvider;
        private readonly CheatsContainer _cheatsContainer;

        public DisposeResourcesState(IAssetProvider assetProvider, CheatsContainer cheatsContainer)
        {
            _assetProvider = assetProvider;
            _cheatsContainer = cheatsContainer;
        }

        protected override void OnEnter()
        {
            _cheatsContainer.Deactivate();
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