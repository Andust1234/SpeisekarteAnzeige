using System.Collections;
using MySql.Data.MySqlClient;
using UnityEngine;

public class MySQLController : MonoBehaviour
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
        if (conn == null)
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
        if (conn != null)
        {
            conn.Close();
        }
    }

    public void InsertDB(string sqlText)
    {
        string cmdText = sqlText;

        if (conn != null)
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

    public object[,] ReadDB(string select, string from)
    {
        string cmdText = "SELECT " + select + " FROM " + from;
        int columnsCount = CountDB(from);
        
        if (conn != null)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = cmdText;
            MySqlDataReader reader = cmd.ExecuteReader();

            object[,] table = new object[columnsCount, reader.FieldCount];

            int i = 0;
            int j = 0;

            while (reader.Read())
            {
                while(i < reader.FieldCount)
                {
                    table[j, i] = reader[i];

                    i++;
                }

                i = 0;
                j++;
            }

            reader.Close();

            return table;
        }

        return null;
    }

    public int CountDB(string table)
    {

        string cmdText = "SELECT COUNT(*) FROM " + table;

        if (conn != null)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = cmdText;
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
        return 0;
    }

}
