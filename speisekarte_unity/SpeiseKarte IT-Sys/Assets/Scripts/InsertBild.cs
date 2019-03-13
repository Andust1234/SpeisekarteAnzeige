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

    public void InsertBildInDatenbank(string sqlText, byte[] imgData)
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
        conn.Open();

        MySqlCommand cmd = conn.CreateCommand();

        cmd.CommandText = sqlText;
        //cmd.Prepare();

        //cmd.Parameters.Add("@img", MySqlDbType.Binary, imgData.Length);
        //cmd.Parameters["@img"].Value = imgData;

        MySqlParameter paramImage = new MySqlParameter("@img", MySqlDbType.Blob, imgData.Length);
        paramImage.Value = imgData;
        cmd.Parameters.Add(paramImage);

        cmd.ExecuteNonQuery();

        conn.Close();        
    }
}
