using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class TestInsertBild : MonoBehaviour
{
    private InsertBild inserBild;

    public Texture2D img;
    public string sqlText;

    private void Start()
    {
        inserBild = this.gameObject.AddComponent<InsertBild>();
        inserBild.InsertBildInDatenbank(sqlText, img);
    }

}
