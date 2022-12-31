#nullable enable
using UnityEngine;

namespace Solar2048.Buildings
{
    public sealed class BuildingBehaviour : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _image = null!;

        public void SetImage(Sprite image) => _image.sprite = image;
    }
}