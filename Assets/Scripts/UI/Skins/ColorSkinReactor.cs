﻿#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace Solar2048.UI.Skins
{
    public sealed class ColorSkinReactor : SkinReactor
    {
        [SerializeField]
        private Image _target = null!;

        [SerializeField]
        private Color[] _colors = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _colors.Length)
            {
                Debug.LogError($"Not enough colors for {nameof(ColorSkinReactor)}");
                return;
            }

            _target.color = _colors[skinID];
        }
    }
}