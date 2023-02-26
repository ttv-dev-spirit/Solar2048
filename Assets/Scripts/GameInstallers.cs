#nullable enable
using System.Collections.Generic;
using Solar2048.Buildings;
using Solar2048.Buildings.Effect;
using Solar2048.Buildings.UI;
using Solar2048.Cards;
using Solar2048.Map;
using Solar2048.Packs;
using Solar2048.Score;
using Solar2048.StateMachine;
using Solar2048.StateMachine.States;
using Solar2048.UI;
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
        private BuildingImage _buildingImagePrefab = null!;

        [SerializeField]
        private Hand _hand = null!;

        [SerializeField]
        private MouseOverObserver _mouseOverObserver = null!;

        [SerializeField]
        private MoveController _moveController = null!;

        [SerializeField]
        private ScoreSettings _scoreSettings = null!;

        [SerializeField]
        private PackGeneratorSettings _packGeneratorSettings = null!;

        [SerializeField]
        private UIManagerSettings _uiManagerSettings = null!;

        [SerializeField]
        private PackBuyingSettings _packBuyingSettings = null!;

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
            Container.BindFactory<BuildingType, BuildingImage, BuildingImage.Factory>()
                .FromComponentInNewPrefab(_buildingImagePrefab);
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
            Container.Bind<ScoreCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PackGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsPackProvider>().AsSingle();
            Container.Bind<UIManager>().AsSingle();
            Container.Bind<PackForScoreBuyer>().AsSingle();
            Container.BindInterfacesAndSelfTo<CardPlayer>().AsSingle();
            Container.Bind<DirectionRoller>().AsSingle();
        }

        private void BindGameObjects()
        {
            Container.Bind<GameFieldBehaviour>().FromInstance(_gameFieldBehaviour);
            Container.Bind<MoveController>().FromInstance(_moveController);
            Container.Bind<MouseOverObserver>().FromInstance(_mouseOverObserver);
        }

        private void BindSettings()
        {
            Container.BindInterfacesAndSelfTo<BuildingFactorySettings>().FromInstance(_buildingFactorySettings);
            Container.Bind<ScoreSettings>().FromInstance(_scoreSettings);
            Container.Bind<PackGeneratorSettings>().FromInstance(_packGeneratorSettings);
            Container.Bind<UIManagerSettings>().FromInstance(_uiManagerSettings);
            Container.Bind<PackBuyingSettings>().FromInstance(_packBuyingSettings);
        }
    }
}