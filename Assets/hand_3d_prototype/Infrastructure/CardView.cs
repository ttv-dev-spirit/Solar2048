#nullable enable
using UnityEngine;

namespace hand_3d_prototype.Infrastructure
{
    public class CardView : MonoBehaviour
    {
        private Vector3 _defaultPosition;
        
        [SerializeField]
        private GameObject _view = null!;

        [SerializeField]
        private Vector3 _defaultSize;

        public GameObject View => _view;
        public Vector3 DefaultSize => _defaultSize;
        public Vector3 DefaultPosition => _defaultPosition;

        private void Awake()
        {
            _defaultPosition = transform.position;
        }
    }
}