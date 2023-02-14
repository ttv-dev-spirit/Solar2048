#nullable enable
using Solar2048.Packs;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048.UI
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