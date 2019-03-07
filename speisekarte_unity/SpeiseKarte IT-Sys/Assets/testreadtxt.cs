using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class testreadtxt : MonoBehaviour
{

    public TextAsset txt;
    public TextAsset txt2;

    private void OnEnable()
    {
        Debug.Log(AssetDatabase.GetAssetPath(txt));
        txt2 = Resources.Load<TextAsset>("Config/MySQL_Config");

        
    }

}
