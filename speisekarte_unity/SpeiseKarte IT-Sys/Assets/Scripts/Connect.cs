using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class Connect : MonoBehaviour
{
    private readonly MySqlConnection conn;
    private MySQLConfig config;

    public Connect()
    {
        config = MySQLConfig.CreateFromJSON();

        if (conn == null)
        {
            try
            {
                conn = new MySqlConnection("SERVER=" + config.host + ";DATABASE=" + config.database + ";UID=" + config.user + ";PASSWORD=" + config.password);
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
}
