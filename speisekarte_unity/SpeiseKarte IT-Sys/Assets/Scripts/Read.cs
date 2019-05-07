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

    public Speise[] ReadSpeisen(int id)
    {
        //D:\xampp\mysql\bin\my.ini
        //innodb_buffer_pool_size = 512M
        //innodb_additional_mem_pool_size = 2M
        //## Set .._log_file_size to 25 % of buffer pool size
        //innodb_log_file_size = 128M
        //innodb_log_buffer_size = 128M

        string sqlText = "SELECT speisekarte.ID, speisekarte.Titel, speisekarte.Preis, speisekarte.Beschreibung, speisekarte.SpeisenArt_ID, speisenart.SpeisenArtName, speisekarte.BildRawData, speisekarte.BildHeight, speisekarte.BildWidth FROM speisekarte INNER JOIN speisenart ON " + id + " = speisenart.ID WHERE speisekarte.SpeisenArt_ID =" + id;

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

        Speise[] speisen = new Speise[k];

        int i = 0;

        while (reader.Read())
        {
            Speise speise = new Speise();
            BildRawData bild = new BildRawData();

            speise.ID = (int)reader[0];
            speise.Titel = (string)reader[1];
            speise.Preis = (string)reader[2];
            speise.Beschreibung = (string)reader[3];
            speise.SpeisenArt_ID = (int)reader[4];
            speise.SpeisenArt = (string)reader[5];

            bild.BildRaw = (byte[])reader[6];
            bild.BildHight = (int)reader[7];
            bild.BildWidth = (int)reader[8];

            speise.Bild = bild;

            speisen[i] = speise;

            i++;
        }

        reader.Close();
        conn.Close();

        return speisen;
    }

    public int CountRowsWhitID(int id)
    {
        int i = 0;
        long k = 0;

        string sqlText = "SELECT COUNT(*) FROM speisekarte WHERE SpeisenArt_ID=" + id;

        conn.Open();

        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = sqlText;
        MySqlDataReader reader = cmd.ExecuteReader();
        
        while (reader.Read())
        {
            k = (long)reader[0];
        }

        reader.Close();
        conn.Close();

        i = (int)k;

        return i;
    }
}
