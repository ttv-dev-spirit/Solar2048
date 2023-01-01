#nullable enable
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Solar2048
{
    public sealed class MoveController : MonoBehaviour
    {
        private GameField _gameField = null!;

        [SerializeField]
        private Button _moveLeft = null!;

        [SerializeField]
        private Button _moveRight = null!;

        [SerializeField]
        private Button _moveUp = null!;

        [SerializeField]
        private Button _moveDown = null!;

        [Inject]
        private void Construct(GameField gameField)
        {
            _gameField = gameField;
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
            _gameField.MoveBuildings(MoveDirections.Left);
        }

        private void MoveRightClickHandler(Unit _)
        {
            _gameField.MoveBuildings(MoveDirections.Right);
        }

        private void MoveUpClickHandler(Unit _)
        {
            _gameField.MoveBuildings(MoveDirections.Up);
        }

        private void MoveDownClickHandler(Unit _)
        {
            _gameField.MoveBuildings(MoveDirections.Down);
        }
    }
}