using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReadBild : MonoBehaviour
{
    private ReadBild readBild;

    public string sqlText;

    private void Start()
    {
        readBild = this.gameObject.AddComponent<ReadBild>();
        
        this.gameObject.GetComponent<Renderer>().material.mainTexture = readBild.ReadBildFromDatabase(sqlText);
    }

}
