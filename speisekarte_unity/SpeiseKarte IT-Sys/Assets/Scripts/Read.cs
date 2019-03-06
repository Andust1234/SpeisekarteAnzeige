using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Read : MonoBehaviour
{
    private Connect connect;
    private MySqlConnection conn;

    public void ReadTableFromDatabase(string sqlText)
    {
        connect = this.gameObject.AddComponent<Connect>();
        conn = connect.GetConnection();

        if (conn != null)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlText;
            MySqlDataReader reader = cmd.ExecuteReader();

            int i = 0;
            string s = "";

            while (reader.Read())
            {
                while (i < reader.FieldCount)
                {
                    //Muss in ein Objekt array rein.
                    s += reader[i] + "\t";

                    i++;
                }

                Debug.Log(s);
                i = 0;
                s = "";
            }

            reader.Close();
        }
        else
        {
            Debug.Log("Keine Connection!");
        }

        connect.CloseConnection();
    }
}
