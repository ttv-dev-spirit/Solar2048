#nullable enable
using Solar2048.Map;
using UnityEngine;

namespace Solar2048.Input
{
    public sealed class BuildingsMoveHandler : InputHandler
    {
        private readonly BuildingMover _buildingMover;

        public BuildingsMoveHandler(BuildingMover buildingMover)
        {
            _buildingMover = buildingMover;
        }

        public override void HandleInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.W) || UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
            {
                _buildingMover.MoveBuildings(MoveDirections.Up);
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.S) || UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
            {
                _buildingMover.MoveBuildings(MoveDirections.Down);
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.A) || UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _buildingMover.MoveBuildings(MoveDirections.Left);
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.D) || UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
            {
                _buildingMover.MoveBuildings(MoveDirections.Right);
            }
        }
    }
}