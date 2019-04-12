using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using UnityEditor;

public class InsertBild : MonoBehaviour
{
    private Connect connect;
    private MySqlConnection conn;

    private void Awake()
    {
        if (GetComponent<Connect>() == null)
        {
            connect = this.gameObject.AddComponent<Connect>();
        }
        else
        {
            connect = this.gameObject.GetComponent<Connect>();
        }

        conn = connect.GetConnection();
    }

    public void InsertBildInDatenbank(string sqlText, byte[] imageData)
    {
        conn.Open();

        MySqlCommand cmd = conn.CreateCommand();

        cmd.CommandText = sqlText;

        MySqlParameter paramImage = new MySqlParameter("@img", MySqlDbType.Blob, imageData.Length);
        paramImage.Value = imageData;
        cmd.Parameters.Add(paramImage);

        cmd.ExecuteNonQuery();

        conn.Close();        
    }
}
