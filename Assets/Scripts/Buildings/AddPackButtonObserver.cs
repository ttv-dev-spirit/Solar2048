#nullable enable
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048.Buildings
{
    public sealed class AddPackButtonObserver : MonoBehaviour
    {
        private PackForScoreBuyer _packForScoreBuyer = null!;

        [SerializeField]
        private GameObject _button = null!;

        [Inject]
        private void Construct(PackForScoreBuyer packForScoreBuyer)
        {
            _packForScoreBuyer = packForScoreBuyer;
        }

        private void Start()
        {
            _packForScoreBuyer.IsEnoughPointsForPack.Subscribe(UpdateState);
        }

        private void UpdateState(bool isEnoughPointsForPack) => _button.SetActive(isEnoughPointsForPack);
    }
}