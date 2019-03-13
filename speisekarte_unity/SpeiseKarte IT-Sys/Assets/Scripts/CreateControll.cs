using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.IO;

public class CreateControll : MonoBehaviour
{
    public GameObject vorschauBild;
    public Text titel;
    public Text beschreibung;
    public Text preis;

    private byte[] imageData = null;
    private Texture2D txt;
    private Sprite sprite;
    private Insert insert;
    private InsertBild insertBild;

    [DllImport("user32.dll")]
    private static extern void OpenFileDialog();

    public void BildVorschau()
    {
        System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

        ofd.Filter = "Image files (*.jpg;*.png)|*.jpg;*png";

        ofd.ShowDialog();

        imageData = File.ReadAllBytes(ofd.FileName);

        txt = new Texture2D(2, 2);

        txt.LoadImage(imageData);

        sprite = Sprite.Create(txt, new Rect(0, 0, txt.width, txt.height), new Vector2(0.5f, 0.5f), 1f);

        vorschauBild.GetComponent<Image>().sprite = sprite;

        VorschaubildSeitenverhältnis();
    }

    private void VorschaubildSeitenverhältnis()
    {
        float width = 125;
        float height = 125;
        float einsEntspricht = 0;

        if(txt.width > txt.height)
        {
            einsEntspricht = (float)125 / txt.width;
            height = txt.height * einsEntspricht;
        }
        else if(txt.height > txt.width)
        {
            einsEntspricht = (float)125 / txt.height;
            width = txt.width * einsEntspricht;
        }

            vorschauBild.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public void Save()
    {
        insert = this.gameObject.AddComponent<Insert>();
        insertBild = this.gameObject.AddComponent<InsertBild>();

        string sqlText = "INSERT INTO speisekarte(titel, preis, beschreibung) " +
            "VALUES('" + titel.text + "', '" + preis.text + "', '" + beschreibung.text + "')";

        insert.InsertToDatabase(sqlText);

        string sqlTextBild = "UPDATE speisekarte SET bild = @img WHERE id = " + insert.GetLastInsertedId();

        insertBild.InsertBildInDatenbank(sqlTextBild, imageData);
    }
}
