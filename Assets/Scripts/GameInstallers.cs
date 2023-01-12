#nullable enable
using System.Collections.Generic;
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

        [SerializeField]
        private Card _cardPrefab = null!;

        [SerializeField]
        private Hand _hand = null!;

        [SerializeField]
        private CheatHand _cheatHand = null!;

        [SerializeField]
        private MoveController _moveController = null!;

        [SerializeField]
        private ScoreSettings _scoreSettings = null!;

        [SerializeField]
        private PackGeneratorSettings _packGeneratorSettings = null!;

        public override void InstallBindings()
        {
            BindSingles();
            BindGameObjects();
            BindSettings();
            BindFactories();
        }

        private void BindFactories()
        {
            Container.BindFactory<BuildingBehaviour, BuildingBehaviour.Factory>()
                .FromComponentInNewPrefab(_buildingPrefab);
            Container.BindFactory<BuildingType, Card, Card.Factory>().FromComponentInNewPrefab(_cardPrefab);
            Container.BindFactory<List<BuildingType>, Pack, Pack.Factory>();
        }

        private void BindSingles()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<GameMap>().AsSingle();
            Container.BindInterfacesAndSelfTo<MessageBroker>().AsSingle();
            Container.Bind<BuildingsManager>().AsSingle();
            Container.Bind<BuildingsFactory>().AsSingle();
            Container.Bind<Hand>().FromInstance(_hand);
            Container.Bind<CardSpawner>().AsSingle();
            Container.Bind<BuildingPlacer>().AsSingle().NonLazy();
            Container.Bind<BuildingEffectsTrigger>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle();
            Container.Bind<BuildingMover>().AsSingle();
            Container.Bind<CheatHand>().FromInstance(_cheatHand);
            Container.Bind<ScoreCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PackGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsPackProvider>().AsSingle();
        }

        private void BindGameObjects()
        {
            Container.Bind<GameFieldBehaviour>().FromInstance(_gameFieldBehaviour);
            Container.Bind<MoveController>().FromInstance(_moveController);
        }

        private void BindSettings()
        {
            Container.Bind<BuildingFactorySettings>().FromInstance(_buildingFactorySettings);
            Container.Bind<ScoreSettings>().FromInstance(_scoreSettings);
            Container.Bind<PackGeneratorSettings>().FromInstance(_packGeneratorSettings);
        }
    }
}