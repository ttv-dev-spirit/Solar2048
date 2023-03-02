#nullable enable
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Solar2048.Cycles.UI
{
    public sealed class CyclePanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _cyclesText = null!;

        [Inject]
        private void Construct(ICycleCounter cycleCounter)
        {
            cycleCounter.CycleCount.Subscribe(OnCycleUpdate);
        }

        private void OnCycleUpdate(int cycle) => _cyclesText.text = cycle.ToString();
    }
}