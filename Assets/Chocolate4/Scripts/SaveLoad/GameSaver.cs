using UnityEngine;
using System.IO;

namespace Chocolate4.SaveLoad
{
    public class GameSaver
    {
        private string savePath;

        public GameSaver()
        {
            savePath = Path.Combine(Application.persistentDataPath, "SaveFile");
        }
        public bool FileExists() => File.Exists(savePath);
        public void Save(IPersistantObject game, int saveVersion)
        {
            using (
                var writer = new BinaryWriter(File.Open(savePath, FileMode.Create))
            ) 
            {
                writer.Write(saveVersion);
                game.Save(new GameDataWriter(writer));
            }
        }
        public void Load(IPersistantObject game)
        {
            if (!FileExists()) return;
            using (
                var reader = new BinaryReader(File.Open(savePath, FileMode.Open))
            ) 
            {

                game.Load(new GameDataReader(reader, reader.ReadInt32()));
            }
        }
    }
}