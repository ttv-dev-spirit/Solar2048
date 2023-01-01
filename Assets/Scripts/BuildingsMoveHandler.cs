#nullable enable
using UnityEngine;

namespace Solar2048
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
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _buildingMover.MoveBuildings(MoveDirections.Up);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _buildingMover.MoveBuildings(MoveDirections.Down);
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _buildingMover.MoveBuildings(MoveDirections.Left);
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _buildingMover.MoveBuildings(MoveDirections.Right);
            }
        }
    }
}