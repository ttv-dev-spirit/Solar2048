#nullable enable
using UnityEngine;

namespace Solar2048
{
    public sealed class BuildingsMoveHandler : InputHandler
    {
        private readonly GameField _gameField;

        public BuildingsMoveHandler(GameField gameField)
        {
            _gameField = gameField;
        }

        public override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _gameField.MoveBuildings(MoveDirections.Up);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _gameField.MoveBuildings(MoveDirections.Down);
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _gameField.MoveBuildings(MoveDirections.Left);
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _gameField.MoveBuildings(MoveDirections.Right);
            }
        }
    }
}