using UnityEngine;

public static class PlayerPrefsManager
{
    public static readonly string CurrentLevel = "CurrentLevel";
    public static readonly string HighScore = "HighScore";
    public static void Set<T>(string key, T value)
    {
        if (typeof(T) == typeof(int))
        {
            PlayerPrefs.SetInt(key, (int)(object)value);
        }
        else if (typeof(T) == typeof(float))
        {
            PlayerPrefs.SetFloat(key, (float)(object)value);
        }
        else if (typeof(T) == typeof(string))
        {
            PlayerPrefs.SetString(key, (string)(object)value);
        }
        else
        {
            Debug.LogWarning("Unsupported data type for PlayerPrefsManager.Set");
        }
        PlayerPrefs.Save();

        Debug.Log("Player Pref Value "+key+" "+value);
    }

    public static T Get<T>(string key, T defaultValue)
    {
        if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(key, (int)(object)defaultValue);
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(key, (float)(object)defaultValue);
        }
        else if (typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(key, (string)(object)defaultValue);
        }
        else
        {
            Debug.LogWarning("Unsupported data type for PlayerPrefsManager.Get");
            return defaultValue;
        }
        Debug.Log("Player Pref Value " + key + " " + defaultValue);
    }
}
