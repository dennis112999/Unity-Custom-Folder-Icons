using System.Collections.Generic;
using System;
using UnityEngine;

#if UNITY_EDITOR

namespace Dennis.Tools.MineralEditor
{
    [Serializable]
    public class KeyValuePairList
    {
        public string Key;
        public string Value;

        public KeyValuePairList(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }


    [Serializable]
    public class MineralData
    {
        [SerializeField]
        private List<KeyValuePairList> keyValuePairs;

        private SortedDictionary<string, string> keyValueDictionary;

        public MineralData()
        {
            keyValuePairs = new List<KeyValuePairList>();
            keyValueDictionary = new SortedDictionary<string, string>();
        }

        public string GetString(string key, string defaultValue)
        {
            if (keyValueDictionary.TryGetValue(key, out string value))
            {
                return value;
            }

            return defaultValue;
        }

        public void SetString(string key, string value)
        {
            if (keyValueDictionary.ContainsKey(key))
            {
                keyValueDictionary[key] = value;
                UpdateList(key, value);
            }
            else
            {
                keyValueDictionary[key] = value;
                keyValuePairs.Add(new KeyValuePairList(key, value));
            }
        }

        public void DeleteKey(string key)
        {
            if (keyValueDictionary.Remove(key))
            {
                keyValuePairs.RemoveAll(pair => pair.Key == key);
            }
            else
            {
                Debug.LogError($"Key '{key}' not found in key-value pairs.");
            }
        }

        private void UpdateList(string key, string value)
        {
            foreach (var pair in keyValuePairs)
            {
                if (pair.Key == key)
                {
                    pair.Value = value;
                    return;
                }
            }
            keyValuePairs.Add(new KeyValuePairList(key, value));
        }

        public void ConvertToDictionary()
        {
            keyValueDictionary = new SortedDictionary<string, string>();
            foreach (var pair in keyValuePairs)
            {
                keyValueDictionary[pair.Key] = pair.Value;
            }
        }
    }
}

#endif