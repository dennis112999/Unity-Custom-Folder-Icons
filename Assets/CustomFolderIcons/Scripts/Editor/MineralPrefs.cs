using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;

namespace Dennis.Tools.MineralEditor
{
    [InitializeOnLoad]
    public static class MineralPrefs
    {
        const string dataPath = "Assets/CustomFolderIcons/Data/MineralData.txt";
        static MineralData dataObject;

        static MineralPrefs()
        {
            LoadData();
        }

        private static void LoadData()
        {
            if (!File.Exists(dataPath))
            {
                dataObject = new MineralData();
                SaveData();
            }
            else
            {
                string data = File.ReadAllText(dataPath);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    dataObject = JsonUtility.FromJson<MineralData>(data);
                    dataObject.ConvertToDictionary();
                }
            }
        }

        private static void SaveData()
        {
            if (dataObject == null) dataObject = new MineralData();
            string data = JsonUtility.ToJson(dataObject, true);
            File.WriteAllText(dataPath, data);
        }

        public static string GetString(string key, string defaultValue)
        {
            return dataObject.GetString(key, defaultValue);
        }

        public static void SetString(string key, string value)
        {
            dataObject.SetString(key, value);
            SaveData();
        }

        public static void DeleteKey(string key)
        {
            dataObject.DeleteKey(key);
            SaveData();
        }
    }
}
#endif