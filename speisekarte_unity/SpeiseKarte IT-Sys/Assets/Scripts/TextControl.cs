using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextControl : MonoBehaviour
{
    public MySQLController other;
    private int currentRowCount = 0;
    private string sqlText = "SELECT * FROM test";
    private string newText;

    void Update()
    {
        if (other.CountDB("test") != currentRowCount)
        {
            currentRowCount = other.CountDB("test");

            newText = "";

            object[,] table = other.ReadDB("*", "test");//hier muss jetzt der string[,] ausgelesen und dargestellt werden

            Debug.Log(table.GetLength(0));
            Debug.Log(table.GetLength(1));

            for (int j = 0; j < table.GetLength(0); j++)
            {
                for (int i = 0; i < table.GetLength(1); i++)
                {
                    newText += table[j, i] + "\t";
                }

                newText += "\n";
            }

            this.gameObject.GetComponent<Text>().text = newText;
        }
    }
}
//SELECT speisen.ID, speisen.Name, speisenart.name FROM speisen INNER JOIN speisenart ON speisen.Speisenart_id = speisenart.speisenart_id;