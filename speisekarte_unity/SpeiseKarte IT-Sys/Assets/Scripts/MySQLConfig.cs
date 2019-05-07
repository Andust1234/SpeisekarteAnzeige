using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class MySQLConfig
{
    public string host;
    public string database;
    public string user;
    public string password;

    public static MySQLConfig CreateFromJSON(string jsonTextFile)
    {
        return JsonUtility.FromJson<MySQLConfig>(jsonTextFile);
    }

    //public void WriteToJSON()
    //{
    //    string configToJson = JsonUtility.ToJson(this);

    //    File.WriteAllText("Assets/Config/MySQL.json", configToJson);
    //}
}
