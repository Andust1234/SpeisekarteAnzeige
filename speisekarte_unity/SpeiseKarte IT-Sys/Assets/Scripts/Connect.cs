using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class Connect : MonoBehaviour
{
    private string host, database, user, password;

    private MySqlConnection conn;

    public Connect()
    {
        ReadMySQLConfig();
    }

    private void ReadMySQLConfig()
    {
        string[] lines = File.ReadAllText("Assets/Resources/Config/MySQL_Config.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        host = lines[0];
        database = lines[1];
        user = lines[2];
        password = lines[3];

        SetupConnection();
    }

    private void SetupConnection()
    {
        if (conn == null)
        {
            string connString = "SERVER=" + host + ";DATABASE=" + database + ";UID=" + user + ";PASSWORD=" + password;

            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();

                //Debug.Log("Connected.");
            }
            catch (MySqlException ex)
            {
                Debug.Log("MySQL Error: " + ex.ToString());
            }
        }
    }

    public MySqlConnection GetConnection()
    {
        return conn;
    }

    public void CloseConnection()
    {
        conn.Close();
    }
}
