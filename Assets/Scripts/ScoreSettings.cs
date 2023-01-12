#nullable enable
using UnityEngine;

namespace Solar2048
{
    [CreateAssetMenu(menuName = "Configs/Create ScoreSettings", fileName = "score_settings", order = 0)]
    public sealed class ScoreSettings : ScriptableObject
    {
        [SerializeField]
        private int _mergeScore = 1;

        public int MergeScore => _mergeScore;
    }
}