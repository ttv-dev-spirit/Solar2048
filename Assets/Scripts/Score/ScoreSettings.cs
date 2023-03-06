#nullable enable
using UnityEngine;

namespace Solar2048.Score
{
    [CreateAssetMenu(menuName = "Configs/Score/score_setting", fileName = "score_settings", order = 0)]
    public sealed class ScoreSettings : ScriptableObject
    {
        [SerializeField]
        private int _mergeBase = 10;

        [SerializeField]
        private int _perLevelMergeBonus = 10;

        public int GetMergeScore(int mergeResultLevel) => _mergeBase + _perLevelMergeBonus * (mergeResultLevel - 1);
    }
}