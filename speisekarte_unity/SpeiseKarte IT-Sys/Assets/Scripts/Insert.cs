using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Insert : MonoBehaviour
{
    private Connect connect;
    private MySqlConnection conn;
    private MySqlCommand cmd;

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

        cmd = conn.CreateCommand();
    }

    public void InsertSpeiseInDatabase(Speise speise)
    {
        string sqlText = "INSERT INTO speisekarte(titel, preis, beschreibung, speisenart_id, bildrawdata, bildheight, bildwidth)" +
            " VALUES(@titel, @preis, @beschreibung, @speisenart_id, @bildrawdata, @bildheight, @bildwidth)";

        ExecuteSqlText(sqlText, speise);
    }

    public void UpdateSpeiseInDatabaseWhereID(Speise speise)
    {
        string sqlText = "UPDATE speisekarte SET titel=@titel, preis=@preis, beschreibung=@beschreibung, speisenart_id=@speisenart_id," +
            " bildrawdata=@bildrawdata, bildheight=@bildheight, bildwidth=@bildwidth WHERE ID=" + speise.ID;

        ExecuteSqlText(sqlText, speise);
    }

    private void ExecuteSqlText(string sqlText, Speise speise)
    {
        conn.Open();

        cmd.CommandText = "SET GLOBAL max_allowed_packet=1024*1024*1024;";
        cmd.ExecuteNonQuery();

        cmd.CommandText = sqlText;

        MySqlParameter paramTitel = new MySqlParameter("@titel", MySqlDbType.Text);
        MySqlParameter paramPreis = new MySqlParameter("@preis", MySqlDbType.Text);
        MySqlParameter paramBeschreibung = new MySqlParameter("@beschreibung", MySqlDbType.Text);
        MySqlParameter paramSpeisenart_id = new MySqlParameter("@speisenart_id", MySqlDbType.Int16);
        MySqlParameter paramBildRawData = new MySqlParameter("@bildrawdata", MySqlDbType.Blob);
        MySqlParameter paramBildHeight = new MySqlParameter("@bildheight", MySqlDbType.Int16);
        MySqlParameter paramBildWidth = new MySqlParameter("@bildwidth", MySqlDbType.Int16);

        paramTitel.Value = speise.Titel;
        paramPreis.Value = speise.Preis;
        paramBeschreibung.Value = speise.Beschreibung;
        paramSpeisenart_id.Value = speise.SpeisenArt_ID;
        paramBildRawData.Value = speise.Bild.BildRaw;
        paramBildHeight.Value = speise.Bild.BildHight;
        paramBildWidth.Value = speise.Bild.BildWidth;

        cmd.Parameters.Add(paramTitel);
        cmd.Parameters.Add(paramPreis);
        cmd.Parameters.Add(paramBeschreibung);
        cmd.Parameters.Add(paramSpeisenart_id);
        cmd.Parameters.Add(paramBildRawData);
        cmd.Parameters.Add(paramBildHeight);
        cmd.Parameters.Add(paramBildWidth);

        cmd.ExecuteNonQuery();

        conn.Close();
    }
}
