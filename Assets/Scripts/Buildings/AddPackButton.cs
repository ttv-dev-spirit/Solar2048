#nullable enable
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Buildings
{
    public sealed class AddPackButton : MonoBehaviour
    {
        private PackForScoreBuyer _packForScoreBuyer = null!;

        [SerializeField]
        private Button _button = null!;

        [Inject]
        private void Construct(PackForScoreBuyer packForScoreBuyer)
        {
            _packForScoreBuyer = packForScoreBuyer;
        }

        private void Start()
        {
            _button.OnClickAsObservable().Subscribe(OnClickHandler);
        }

        private void OnClickHandler(Unit _) => _packForScoreBuyer.BuyPack();
    }
}