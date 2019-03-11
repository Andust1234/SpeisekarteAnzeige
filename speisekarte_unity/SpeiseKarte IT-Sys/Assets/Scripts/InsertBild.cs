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

    public void InsertBildInDatenbank(string sqlText, Texture2D img)
    {
        byte[] data = null;

        connect = this.gameObject.AddComponent<Connect>();
        conn = connect.GetConnection();
        conn.Open();

        data = File.ReadAllBytes(AssetDatabase.GetAssetPath(img));//Muss der Path zum Bild rein weil AssetDatabase nur im Editor funtioniert

        MySqlCommand cmd = conn.CreateCommand();

        cmd.CommandText = sqlText;
        cmd.Prepare();

        cmd.Parameters.Add("@img", MySqlDbType.Binary, data.Length);
        cmd.Parameters["@img"].Value = data;
        cmd.ExecuteNonQuery();

        conn.Close();        
    }
}
