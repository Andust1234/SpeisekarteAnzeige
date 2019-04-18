using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Read : MonoBehaviour
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

    public SpeiseArt[] ReadSpeisenArtTable(string sqlText)
    {
        conn.Open();

        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = sqlText;
        MySqlDataReader reader = cmd.ExecuteReader();

        int k = 0;

        while (reader.Read())
        {
            k++;
        }

        reader.Close();

        cmd.CommandText = sqlText;
        reader = cmd.ExecuteReader();

        SpeiseArt[] table = new SpeiseArt[k];

        int i = 0;

        while (reader.Read())
        {
            SpeiseArt speisenArtTable = new SpeiseArt();

            speisenArtTable.ID = (int)reader[0];
            speisenArtTable.SpeisenArt = (string)reader[1];

            table[i] = speisenArtTable;

            i++;
        }

        reader.Close();
        conn.Close();

        return table;
    }

    public Speise[] ReadSpeiseTable(string sqlText)
    {
        conn.Open();

        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = sqlText;
        MySqlDataReader reader = cmd.ExecuteReader();

        int k = 0;

        while (reader.Read())
        {
            k++;
        }

        reader.Close();

        cmd.CommandText = sqlText;
        reader = cmd.ExecuteReader();

        Speise[] table = new Speise[k];

        int i = 0;

        while (reader.Read())
        {
            Speise speisenTable = new Speise();

            speisenTable.ID = (int)reader[0];
            speisenTable.Titel = (string)reader[1];
            //speisenTable.Bild = (byte[])reader[2];
            speisenTable.Preis = (string)reader[3];
            speisenTable.Beschreibung = (string)reader[4];
            speisenTable.SpeisenArt = (string)reader[5];
            speisenTable.SpeisenArt_ID = (int)reader[6];

            table[i] = speisenTable;

            i++;
        }

        reader.Close();
        conn.Close();

        return table;
    }
}
