#nullable enable

using System;
using System.Collections.Generic;
using Solar2048.AssetManagement;
using UnityEngine;

namespace Solar2048.SaveLoad
{
    public sealed class SaveController
    {
        private readonly List<ISavable> _savables = new();
        private readonly List<ILoadable> _loadables = new();
        private readonly DataToFileWriter _dataToFileWriter;

        private GameData _gameData = new();

        public SaveController(DataToFileWriter dataToFileWriter)
        {
            _dataToFileWriter = dataToFileWriter;
        }

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
            _dataToFileWriter.WriteData(json);
        }

        public bool LoadGame()
        {
            if (!_dataToFileWriter.DoesFileExist())
            {
                return false;
            }

            if (!_dataToFileWriter.TryReadData(out string data))
            {
                return false;
            }

            try
            {
                _gameData = JsonUtility.FromJson<GameData>(data);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }

            foreach (ILoadable loadable in _loadables)
            {
                loadable.Load(_gameData);
            }

            return true;
        }

        // TODO (Stas): Seems too heavy.
        public bool IsSaveAvailable()
        {
            if (!_dataToFileWriter.DoesFileExist())
            {
                return false;
            }

            if (!_dataToFileWriter.TryReadData(out string data))
            {
                return false;
            }

            try
            {
                JsonUtility.FromJson<GameData>(data);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }

            return true;
        }
    }
}