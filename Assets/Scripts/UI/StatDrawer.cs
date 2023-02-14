#nullable enable
using TMPro;
using UniRx;
using UnityEngine;

namespace Solar2048.UI
{
    public sealed class StatDrawer : MonoBehaviour
    {
        [SerializeField]
        private bool _hideIfNull = false;

        [SerializeField]
        private TMP_Text _value = null!;

        public void Subscribe(IReadOnlyReactiveProperty<int> stat)
        {
            stat.Subscribe(UpdateStat);
        }

        private void UpdateStat(int value)
        {
            if (value == 0 && _hideIfNull)
            {
                _value.gameObject.SetActive(false);
                return;
            }

            _value.gameObject.SetActive(true);
            _value.text = value.ToString();
        }
    }
}