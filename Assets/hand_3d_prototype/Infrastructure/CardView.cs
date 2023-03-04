#nullable enable
using UnityEngine;

namespace hand_3d_prototype.Infrastructure
{
    public class CardView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _view = null!;

        [SerializeField]
        private Vector3 _defaultSize;

        public GameObject View => _view;
    }
}