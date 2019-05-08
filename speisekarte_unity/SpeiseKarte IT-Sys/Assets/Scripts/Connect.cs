using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using UnityEngine.UI;

public class Connect : MonoBehaviour
{
    private MySqlConnection conn;
    private MySQLConfig config;
    private string connectString;

    public void Awake()
    {
        string jsonTextFile = File.ReadAllText(Application.dataPath + "/Resources/MySQL.json");

        config = MySQLConfig.CreateFromJSON(jsonTextFile);

        connectString = "SERVER=" + config.host + ";DATABASE=" + config.database + ";UID=" + config.user + ";PASSWORD=" + config.password;

        ConnectDB();
    }

    private void ConnectDB()
    {
        if (conn == null)
        {
            try
            {
                conn = new MySqlConnection(connectString);
            }
            catch (MySqlException ex)
            {
                Debug.Log("MySQL Error: " + ex.ToString());
            }
        }
    }

    public bool TryConnection()
    {
        bool IsConn = false;
        
        try
        {
            string str = "SELECT * FROM speisekarte WHERE 1 LIMIT 1";

            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = str;
            MySqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
            }

            reader.Close();

            IsConn = true;

            conn.Close();
        }
        catch(Exception)
        {
            IsConn = false;
        }

        return IsConn;
    }

    public MySqlConnection GetConnection()
    {
        return conn;
    }
}
