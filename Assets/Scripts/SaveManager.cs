using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
public static class SaveManager
{
    private static string Path =>
        Application.persistentDataPath + "/save.json";

    public static void LoadOrCreate()
    {
        if (File.Exists(Path))
        {
            string json = File.ReadAllText(Path);
            SaveData data = JsonConvert.DeserializeObject<SaveData>(json);

            if (data != null && data.CompliteForestLevels != null)
            {
                GameData.CompliteForestLevels = data.CompliteForestLevels;
            }
            else
            {
                GameData.CompliteForestLevels =
                    new Dictionary<string, bool>(DefaultData.CompliteForestLevels);
            }
        }
        else
        {
            GameData.CompliteForestLevels = new Dictionary<string, bool>(DefaultData.CompliteForestLevels);
        }
    }




}
