using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerPrefsSerializer
{
    public static BinaryFormatter bff = new BinaryFormatter();

    public static bool Save(String key, object value)
    {
        Debug.LogError("Saving...");

        if (!(value.GetType().IsSerializable))
        {
            Debug.LogError(value.GetType() + " is not a serializable Type.");
            return false;
        } 

        MemoryStream ms = new MemoryStream();
        bff.Serialize(ms, value);
        string tmp = System.Convert.ToBase64String(ms.ToArray());
        PlayerPrefs.SetString(key, tmp);

        return (PlayerPrefs.GetString(key) != null);
    }

    /*public static object Load<T>(string key)
    {
        Debug.LogError("Loading...");

        if (!PlayerPrefs.HasKey(key))
            return default(T);

        string tmp = PlayerPrefs.GetString(key);
        MemoryStream ms = new MemoryStream();
        T desObj = (T)bff.Deserialize(ms);

        return desObj;
    }*/
}