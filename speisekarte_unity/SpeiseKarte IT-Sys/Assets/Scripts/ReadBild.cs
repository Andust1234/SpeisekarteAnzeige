using System.Collections;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEditor;

public class ReadBild : MonoBehaviour
{
    private Connect connect;
    private MySqlConnection conn;

    public Texture2D ReadBildFromDatabase(string sqlText)
    {
        connect = this.gameObject.AddComponent<Connect>();
        conn = connect.GetConnection();
        conn.Open();

        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = sqlText;

        byte[] data = (byte[])cmd.ExecuteScalar();

        conn.Close();

        Texture2D tex = new Texture2D(2, 2);

        tex.LoadImage(data);

        return tex;
    }
}
