using UnityEngine;

public class DataStore : IDataStore
{
    public T GetData<T>(string fileName)
    {
        var json = PlayerPrefs.GetString(fileName);
        return JsonUtility.FromJson<T>(json);
    }

    public void SetData<T>(T data, string fileName)
    {
        var json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(fileName, json);
        PlayerPrefs.Save();
    }
}
