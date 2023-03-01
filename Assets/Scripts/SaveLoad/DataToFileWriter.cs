#nullable enable
using System.IO;
using UnityEngine;

namespace Solar2048.SaveLoad
{
    public sealed class DataToFileWriter
    {
        private readonly string _savePath = Application.persistentDataPath + @"/Saves/save.sv";

        public bool TryReadData(out string data)
        {
            try
            {
                using var sr = new StreamReader(_savePath);
                data = sr.ReadToEnd();
                return true;
            }
            catch (IOException e)
            {
                Debug.LogError($"The file {_savePath} could not be read.");
                Debug.LogError(e.Message);
            }

            data = null!;
            return false;
        }

        public bool DoesFileExist() => File.Exists(_savePath);

        public void WriteData(string data)
        {
            if (!Directory.Exists(Application.persistentDataPath + @"/Saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + @"/Saves");
            }

            using StreamWriter outputFile = new StreamWriter(_savePath);
            outputFile.Write(data);
        }
    }
}