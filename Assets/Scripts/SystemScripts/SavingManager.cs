using System.IO;
using UnityEngine;

public static class SavingManager
{
    private static readonly string _path = Application.persistentDataPath + "/gamedata.dat";

    public static bool HasDataSaved()
    {
        return File.Exists(_path);
    }

    public static void SaveNew<T>(T data)
    {
        Save<T>(data);
    }

    private static void Save<T>(T obj)
    {
        var jsonString = JsonUtility.ToJson(obj, true);
        File.WriteAllText(_path, jsonString);
        Debug.Log(_path);
    }

    public static T Load<T>()
    {
        var jsonString = File.ReadAllText(_path);
        var objectLoaded = JsonUtility.FromJson<T>(jsonString);
        return objectLoaded;
    }
}