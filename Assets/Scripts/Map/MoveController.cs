#nullable enable
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048.Map
{
    public sealed class MoveController : MonoBehaviour
    {
        private BuildingMover _buildingMover = null!;

        [SerializeField]
        private Button _moveLeft = null!;

        [SerializeField]
        private Button _moveRight = null!;

        [SerializeField]
        private Button _moveUp = null!;

        [SerializeField]
        private Button _moveDown = null!;

        [Inject]
        private void Construct(BuildingMover buildingMover)
        {
            _buildingMover = buildingMover;
        }

        private void Start()
        {
            _moveLeft.OnClickAsObservable().Subscribe(MoveLeftClickHandler);
            _moveRight.OnClickAsObservable().Subscribe(MoveRightClickHandler);
            _moveUp.OnClickAsObservable().Subscribe(MoveUpClickHandler);
            _moveDown.OnClickAsObservable().Subscribe(MoveDownClickHandler);
        }

        private void MoveLeftClickHandler(Unit _)
        {
            _buildingMover.MoveBuildings(MoveDirections.Left);
        }

        private void MoveRightClickHandler(Unit _)
        {
            _buildingMover.MoveBuildings(MoveDirections.Right);
        }

        private void MoveUpClickHandler(Unit _)
        {
            _buildingMover.MoveBuildings(MoveDirections.Up);
        }

        private void MoveDownClickHandler(Unit _)
        {
            _buildingMover.MoveBuildings(MoveDirections.Down);
        }
    }
}