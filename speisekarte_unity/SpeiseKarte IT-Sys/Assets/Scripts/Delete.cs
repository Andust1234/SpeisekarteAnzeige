using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Delete : MonoBehaviour
{
    private Connect connect;
    private MySqlConnection conn;
    private MySqlCommand cmd;

    public void DeleteSpeise(Speise speise)
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

        cmd = conn.CreateCommand();

        string sqlText = "DELETE from speisekarte where id=" + speise.ID;

        conn.Open();

        cmd.CommandText = sqlText;

        cmd.ExecuteNonQuery();

        conn.Close();
    }
}
