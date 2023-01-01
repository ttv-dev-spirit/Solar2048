#nullable enable
using Solar2048.Buildings;
using Solar2048.StateMachine;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048
{
    public sealed class GameInstallers : MonoInstaller
    {
        [SerializeField]
        private BuildingFactorySettings _buildingFactorySettings = null!;

        [SerializeField]
        private GameFieldBehaviour _gameFieldBehaviour = null!;

        [SerializeField]
        private BuildingBehaviour _buildingPrefab = null!;

        public override void InstallBindings()
        {
            BindSingles();
            BindGameObjects();
            BindSettings();
            Container.BindFactory<BuildingBehaviour, BuildingBehaviour.Factory>()
                .FromComponentInNewPrefab(_buildingPrefab);
        }

        private void BindSingles()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<GameField>().AsSingle();
            Container.Bind<MessageBroker>().AsSingle();
            Container.Bind<BuildingsManager>().AsSingle();
            Container.Bind<BuildingsFactory>().AsSingle();
        }

        private void BindGameObjects()
        {
            Container.Bind<GameFieldBehaviour>().FromInstance(_gameFieldBehaviour);
        }

        private void BindSettings()
        {
            Container.Bind<BuildingFactorySettings>().FromInstance(_buildingFactorySettings);
        }
    }
}