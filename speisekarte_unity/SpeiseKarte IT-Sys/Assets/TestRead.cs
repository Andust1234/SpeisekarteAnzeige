using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRead : MonoBehaviour
{
    private Read read;
    public string sqlText;

    private void Start()
    {
        read = this.gameObject.AddComponent<Read>();
        read.ReadTableFromDatabase(sqlText);
    }
}
