#nullable enable
using Solar2048.SaveLoad;

namespace Solar2048.AssetManagement
{
    public interface ISavable
    {
        void Save(GameData gameData);
    }
}