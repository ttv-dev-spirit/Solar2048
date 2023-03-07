#nullable enable
using UnityEngine;

namespace Solar2048.Buildings.UI
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class QuadImage : MonoBehaviour
    {
        private MeshRenderer? _meshRenderer;

        public void SetImage(Sprite sprite)
        {
            if (_meshRenderer == null)
            {
                _meshRenderer = GetComponent<MeshRenderer>();
            }

            Material? material = _meshRenderer.material;
            material.mainTexture = sprite.texture;
        }
    }
}