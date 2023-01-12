#nullable enable
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Buildings
{
    public sealed class SpawnPackTest : MonoBehaviour
    {
        private CardSpawner _cardSpawner = null!;
        private IPackGenerator _packGenerator = null!;

        [SerializeField]
        private Button _button = null!;

        [Inject]
        private void Construct(CardSpawner cardSpawner, IPackGenerator packGenerator)
        {
            _cardSpawner = cardSpawner;
            _packGenerator = packGenerator;
        }

        private void Start()
        {
            _button.OnClickAsObservable().Subscribe(OnClick);
        }

        private void OnClick(Unit _)
        {
            Pack pack = _packGenerator.GetPack();
            foreach (BuildingType packBuildingCard in pack.BuildingCards)
            {
                _cardSpawner.AddCardToHand(packBuildingCard);
            }
        }
    }
}