using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInsert : MonoBehaviour
{
    private Insert insert;
    public string sqlText;

    private void Start()
    {
        insert = this.gameObject.AddComponent<Insert>();
        //insert.InsertToDatabase(sqlText);
    }
}
