using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public MySQLController other;
    public Text input;

    public void Insert()
    {
        string sqlText = "INSERT INTO test (name) VALUE ('" + input.text + "')";

        other.InsertDB(sqlText);
    }
}
