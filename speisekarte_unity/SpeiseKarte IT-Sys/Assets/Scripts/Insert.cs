using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Insert : MonoBehaviour
{
    private Connect connect;
    private MySqlConnection conn;

    public void InsertToDatabase(string sqlText)
    {
        connect = this.gameObject.AddComponent<Connect>();
        conn = connect.GetConnection();

        if (conn != null)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlText;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.Log("MySQL Error: " + ex.ToString());
            }

            //Debug.Log("SQL Text ausgeführt.");
        }
        else
        {
            Debug.Log("Keine Connection!");
        }

        connect.CloseConnection();
    }
}
