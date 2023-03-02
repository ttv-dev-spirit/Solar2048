#nullable enable
using Solar2048.AssetManagement;
using Solar2048.SaveLoad;

namespace Solar2048.Map
{
    public sealed class WindDirectionRoller : IDirectionRoller, ILoadable
    {
        private readonly DirectionRoller _directionRoller;

        private MoveDirection _lastDirection;
        private bool _hasEverRolled;

        public WindDirectionRoller(DirectionRoller directionRoller, SaveController saveController)
        {
            _directionRoller = directionRoller;
            saveController.Register(this);
        }

        public MoveDirection Roll()
        {
            MoveDirection direction = _directionRoller.Roll();
            if (_hasEverRolled)
            {
                while (direction == _lastDirection)
                {
                    direction = _directionRoller.Roll();
                }
            }
            else
            {
                _hasEverRolled = true;
            }

            _lastDirection = direction;
            return direction;
        }

        public void Load(GameData gameData)
        {
            _hasEverRolled = true;
            _lastDirection = gameData.NextDirection;
        }
    }
}