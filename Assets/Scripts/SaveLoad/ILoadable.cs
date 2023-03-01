#nullable enable
using Solar2048.SaveLoad;

namespace Solar2048.AssetManagement
{
    public interface ILoadable
    {
        void Load(GameData gameData);
    }
}