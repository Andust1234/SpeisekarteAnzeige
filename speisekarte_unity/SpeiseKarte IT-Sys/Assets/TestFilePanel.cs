using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class TestFilePanel : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern void OpenFileDialog();

    private Text text;

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();
    }

    public void Apply()
    {


        System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

        //ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        ofd.Filter = "Image files (*.jpg)|*.jpg";

        ofd.ShowDialog();

        text.text = ofd.FileName;


        Debug.Log(ofd.FileName);
    }

}
