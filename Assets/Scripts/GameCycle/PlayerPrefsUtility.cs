using UnityEngine;

public static class PlayerPrefsUtility
{
    public static void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static int? GetInt(string key)
    {
        if (PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetInt(key);
        else
            return null;
    }
}
