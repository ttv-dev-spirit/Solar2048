#nullable enable
using System.Collections.Generic;
using Solar2048.AssetManagement;
using Solar2048.Buildings;
using Solar2048.Buildings.Effect;
using Solar2048.Buildings.UI;
using Solar2048.Cards;
using Solar2048.Cheats;
using Solar2048.Cycles;
using Solar2048.Input;
using Solar2048.Localization;
using Solar2048.Map;
using Solar2048.Packs;
using Solar2048.SaveLoad;
using Solar2048.Score;
using Solar2048.StateMachine;
using Solar2048.StateMachine.Game;
using Solar2048.StateMachine.Game.States;
using Solar2048.StateMachine.Turn;
using Solar2048.StateMachine.Turn.States;
using Solar2048.StaticData;
using Solar2048.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048.Infrastructure
{
    public sealed class GameInstallers : MonoInstaller
    {
        [SerializeField]
        private MapBehaviour _mapBehaviour = null!;

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

        public override void InstallBindings()
        {
            BindSingles();
            BindGameObjects();
            BindFactories();
            BindGameStates();
            BindTurnStates();
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
            Container.Bind(typeof(IGameLifeCycle), typeof(IStateMachine)).To<GameStateMachine>().AsSingle();
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
            Container.Bind(typeof(IScoreCounter), typeof(ScoreCounter)).To<ScoreCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PackGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsPackProvider>().AsSingle();
            Container.Bind<UIManager>().AsSingle();
            Container.Bind<PackForScoreBuyer>().AsSingle();
            Container.BindInterfacesAndSelfTo<CardPlayer>().AsSingle();
            Container.Bind<DirectionRoller>().AsSingle();
            Container.Bind<WindDirectionRoller>().AsSingle();
            Container.Bind<LocalizationController>().AsSingle();
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
            Container.BindInterfacesTo<GameQuitter>().AsSingle();
            Container.Bind<CheatsContainer>().AsSingle();

            Container.BindInterfacesAndSelfTo<StaticDataProvider>().AsSingle();
            Container.Bind<SaveController>().AsSingle();
            Container.BindInterfacesTo<GameStateReseter>().AsSingle();
            Container.Bind<DataToFileWriter>().AsSingle();
            Container.Bind(typeof(ICycleCounter), typeof(CycleCounter)).To<CycleCounter>().AsSingle();
            Container.Bind<ConfirmTurnDispatcher>().AsSingle();
        }

        private void BindGameObjects()
        {
            Container.Bind<MouseOverObserver>().FromInstance(_mouseOverObserver);
            Container.Bind<MapBehaviour>().FromInstance(_mapBehaviour);
        }

        private void BindGameStates()
        {
            Container.Bind<GameStateFactory>().AsSingle();

            Container.BindFactory<NewGameState, NewGameState.Factory>()
                .WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<GameRoundState, GameRoundState.Factory>()
                .WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<DisposeResourcesState, DisposeResourcesState.Factory>()
                .WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<InitializeGameState, InitializeGameState.Factory>()
                .WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainMenuState, MainMenuState.Factory>()
                .WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<LoadGameState, LoadGameState.Factory>()
                .WhenInjectedInto<GameStateFactory>();
        }

        private void BindTurnStates()
        {
            Container.Bind<TurnStateFactory>().AsSingle();

            Container.BindFactory<BotMoveState, BotMoveState.Factory>()
                .WhenInjectedInto<TurnStateFactory>();
            Container.BindFactory<PlayCardState, PlayCardState.Factory>()
                .WhenInjectedInto<TurnStateFactory>();
            Container.BindFactory<GamePauseState, GamePauseState.Factory>()
                .WhenInjectedInto<TurnStateFactory>();
            Container.BindFactory<ConfirmTurnState, ConfirmTurnState.Factory>()
                .WhenInjectedInto<TurnStateFactory>();
        }
    }
}