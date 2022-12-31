#nullable enable
using Solar2048.Buildings;
using Solar2048.StateMachine;
using UniRx;
using Zenject;

namespace Solar2048
{
    public sealed class GameInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<GameField>().AsSingle();
            Container.Bind<MessageBroker>().AsSingle();
            Container.Bind<BuildingsManager>().AsSingle();
        }
    }
}