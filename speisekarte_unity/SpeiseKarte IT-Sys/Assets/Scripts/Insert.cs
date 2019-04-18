using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Insert : MonoBehaviour
{
    private Connect connect;
    private MySqlConnection conn;
    private long lastInsertedId;

    private void Awake()
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
    }

    public void InsertToDatabase(string sqlText)
    {
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
        }
        else
        {
            Debug.Log("Keine Connection!");
        }

        conn.Close();
    }

    public void InsertSpeiseInDatabase(Speise speise)
    {
        string sqlText = "INSERT INTO speisekarte(titel, preis, beschreibung, speiseart_id, bildrawdata, bildheight, bildwidth)" +
            " VALUES(@titel, @preis, @beschreibung, @speiseart_id, @bildrawdata, @bildheight, @bildwidth)";//Hier muss ich weiter machen.
    }

    public long GetLastInsertedId()
    {
        return lastInsertedId;
    }
}
