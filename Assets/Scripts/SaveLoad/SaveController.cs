#nullable enable

using System.Collections.Generic;
using Solar2048.AssetManagement;
using UnityEngine;

namespace Solar2048.SaveLoad
{
    public sealed class SaveController
    {
        private readonly List<ISavable> _savables = new();
        private readonly List<ILoadable> _loadables = new();

        private GameData _gameData = new();

        public void Register(object target)
        {
            if (target is ISavable savable)
            {
                _savables.Add(savable);
            }

            if (target is ILoadable loadable)
            {
                _loadables.Add(loadable);
            }
        }

        public void SaveGame()
        {
            foreach (ISavable savable in _savables)
            {
                savable.Save(_gameData);
            }

            string? json = JsonUtility.ToJson(_gameData);
            Debug.Log($"SAVE: \n {json}");
        }

        public void LoadGame()
        {
            foreach (ILoadable loadable in _loadables)
            {
                loadable.Load(_gameData);
            }
        }

        public void WriteDataToFile(GameData gameData)
        {
        }

        public void ReadDataFromFile()
        {
        }
    }
}