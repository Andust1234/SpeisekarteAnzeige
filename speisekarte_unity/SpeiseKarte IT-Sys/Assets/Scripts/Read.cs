using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Read : MonoBehaviour
{
    private Connect connect;
    private MySqlConnection conn;

    public SpeisenArtTable[] ReadSpeisenArtTable(string sqlText)
    {
        connect = this.gameObject.AddComponent<Connect>();
        conn = connect.GetConnection();
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

        SpeisenArtTable[] table = new SpeisenArtTable[k];

        int i = 0;

        while (reader.Read())
        {
            SpeisenArtTable speisenArtTable = new SpeisenArtTable();

            speisenArtTable.ID = (int)reader[0];
            speisenArtTable.SpeisenArt = (string)reader[1];

            table[i] = speisenArtTable;

            i++;
        }

        reader.Close();
        conn.Close();

        return table;
    }

    public SpeisenTable[] ReadSpeiseTable(string sqlText)
    {
        connect = this.gameObject.AddComponent<Connect>();
        conn = connect.GetConnection();
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

        SpeisenTable[] table = new SpeisenTable[k];

        int i = 0;

        while (reader.Read())
        {
            SpeisenTable speisenTable = new SpeisenTable();

            speisenTable.ID = (int)reader[0];
            speisenTable.Titel = (string)reader[1];
            speisenTable.Bild = (byte[])reader[2];
            speisenTable.Preis = (float)reader[3];
            speisenTable.Beschreibung = (string)reader[4];
            speisenTable.SpeisenArt = (string)reader[5];

            table[i] = speisenTable;

            i++;
        }

        reader.Close();
        conn.Close();

        return table;
    }

    public class SpeisenArtTable
    {
        public int ID { get; set; }
        public string SpeisenArt { get; set; }
    }

    public class SpeisenTable
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public byte[] Bild { get; set; }
        public float Preis { get; set; }
        public string Beschreibung { get; set; }
        public string SpeisenArt { get; set; }
    }
}
