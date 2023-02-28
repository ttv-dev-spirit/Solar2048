#nullable enable
using System;
using UniRx;
using Zenject;

namespace Solar2048.Input
{
    public sealed class InputSystem : ITickable
    {
        private readonly Subject<Unit> _onHandleInput = new();

        public IObservable<Unit> OnHandleInput => _onHandleInput;

        public void Tick()
        {
            _onHandleInput.OnNext(Unit.Default);
        }
    }
}