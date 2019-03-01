using MySql.Data.MySqlClient;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string host, database, user, password;

    private MySqlConnection conn;

    private void Awake()
    {
        SetupConnection();
    }

    private void OnApplicationQuit()
    {
        CloseConnection();
    }

    private void SetupConnection()
    {
        if(conn == null)
        {
            string connString = "SERVER=" + host + ";DATABASE=" + database + ";UID=" + user + ";PASSWORD=" + password;

            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                Debug.Log("MySQL Error: " + ex.ToString());
            }
        }
    }

    private void CloseConnection()
    {
        if(conn != null)
        {
            conn.Close();
        }
    }

    public void TestDB()
    {
        string cmdText = "INSERT INTO test (Name) VALUES ('test')";

        if(conn != null)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = cmdText;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.Log("MySQL Error: " + ex.ToString());
            }
        }
    }

    public void ReadDB()
    {

        string cmdText = "SELECT * FROM test";

        if(conn != null)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = cmdText;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Debug.Log(reader.GetInt16(0) + "  " + reader.GetString(1));
            }
            reader.Close();
        }

    }

}
