using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Insert : MonoBehaviour
{
    private Connect connect;
    private MySqlConnection conn;
    private long lastInsertedId;

    public void InsertToDatabase(string sqlText)
    {
        if(GetComponent<Connect>() == null)
        {
            connect = this.gameObject.AddComponent<Connect>();
        }
        else
        {
            connect = this.gameObject.GetComponent<Connect>();
        }

        conn = connect.GetConnection();
        conn.Open();

        if (conn != null)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlText;

            try
            {
                cmd.ExecuteNonQuery();
                lastInsertedId = cmd.LastInsertedId;
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

        conn.Close();
    }

    public long GetLastInsertedId()
    {
        return lastInsertedId;
    }
}
