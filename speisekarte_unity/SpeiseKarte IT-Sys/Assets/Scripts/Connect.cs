using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEditor;
using System;

public class Connect : MonoBehaviour
{
    public TextAsset MySQLConfig;
    public string host, database, user, password;

    private MySqlConnection conn;

    private void Awake()
    {
        Debug.Log(AssetDatabase.FindAssets("name: MySQL_Config").Length);//Will nicht recht.
        string[] lines = MySQLConfig.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

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

                Debug.Log("Connected.");
            }
            catch (MySqlException ex)
            {
                Debug.Log("MySQL Error: " + ex.ToString());
            }
        }
    }
}
